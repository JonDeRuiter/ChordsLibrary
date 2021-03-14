using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace ChordsLibrary.DataAccess
{

    class ChordDAO
    {
        private string filePath;
        private List<string> body;
        private string lineDelim;
        private string valDelim;
        private string header;
        private string footer;

        private List<Chord> _allChords; //cache chord data while object exists


        public string Header { get { return header; } set { this.header = value; } }
        public string Footer { get { return footer; } set { this.footer = value; } }
        public string ValDelim
        {
            get { return valDelim; }
            set => valDelim = "|";
        }
        public string LineDelim
        {
            get { return lineDelim; }
            set => lineDelim = "*";
        }
        public List<string> Body { get; set; }
        public string FilePath
        {
            get { return filePath; }
            set => filePath = value + ".txt";
            //set => filePath = "~\\ChordLibrary\\Storage\\" + value + ".txt";
        }
        public List<Chord> DBChords { get { return _allChords; } }


        public ChordDAO()
        {
            this.LineDelim = "";
            this.ValDelim = "";
        }
        public ChordDAO(string chordSize)
        {
            this.FilePath = chordSize;
        }

        public List<Chord> GetAllChordData(string chordSize)
        {
            //read data   
            var enviroment = System.Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(enviroment).Parent.FullName;
            projectDirectory += "\\Storage\\" + chordSize;

            string chordString;
            ChordDAO dao = new ChordDAO(projectDirectory);
            chordString = dao.ReadData(dao);

            List<Chord> allChords = new List<Chord>();
            if (chordString != "The file is empty")
            {
                allChords = dao.BodyToChordList(chordString);
                
            }

            allChords = FillNoteLists(allChords);
            this._allChords = allChords;

            return allChords;
        }

        private List<Chord> FillNoteLists(List<Chord> dbChords)
        {
            foreach (Chord chord in dbChords)
	        {
                chord.ChordNoteList = NoteListFromChordName(chord.RootNote, chord.NoteDifference);
	        }
            return dbChords;

        }

        private List<Note> NoteListFromChordName(Note rootNote, int[] noteDif)
        {
            List<Note> noteList = new List<Note>();

            noteList.Add(rootNote);
            for (int i = 1; i <= noteDif.Length; i++)
            {
                noteList.Add(rootNote.GetNoteByIncrement(rootNote, noteDif[i]));
            }

            return noteList;
        }

    public List<Chord> LoadChordsByCount(int count)
        {
            List<Chord> chords = new List<Chord>();

            if (_allChords != null && !(_allChords.Any()) && _allChords[0].ChordNoteList.Count() == count)
            {
                return _allChords;
            }
            else
            {
                chords = GetAllChordData(count.ToString());
            }

            return chords;
        }

        public List<Chord> FindAChord(Chord unknownChord)
        {
            int chordLength = unknownChord.ChordNoteList.Count;
            List<Chord> foundChords = new List<Chord>();
            if (_allChords.Count == 0)
            {
                _allChords = GetAllChordData(chordLength.ToString());
            }

            foreach (Chord co in _allChords)
            {
                if (co.RootNote.Index == unknownChord.RootNote.Index)
                {
                    //comparing int arrays
                    if (co.NoteDifference.SequenceEqual(unknownChord.NoteDifference))
                    {
                        foundChords.Add(co);
                        return foundChords;
                    }
                }
            }

            foundChords = CheckForInversions(unknownChord);

            return foundChords;
        }


        private void EmptyCache()
        {
            _allChords = null;
        }

        private string ReadData(ChordDAO dao)
        {
            string fullFile, header, footer;
            string body = "";
            try
            {
                FileCheck(dao);
                using (StreamReader sr = new StreamReader(dao.FilePath))
                {
                    fullFile = sr.ReadToEnd();
                }

                if (fullFile.Trim() == "")
                {
                    return "The file is empty";
                }
                //maybe have this put into a string[]??
                header = GetHeader(fullFile);
                body = GetBody(fullFile, header.Length);
                footer = GetFooter(fullFile);
            }
            catch (Exception e)
            {
                return e.ToString();
                //TODO: Some sort of logging?
            }
            //get rid of LineDelim
            return body + LineDelim;
        }

        private string GetHeader(string fullFile)
        {
            string header;

            int headerEnd = fullFile.IndexOf("$$") + 2;
            header = fullFile.Substring(0, headerEnd);

            return header;
        }

        private string GetBody(string fullFile, int endOfHeader)
        {
            string body;

            int bodyEnd = fullFile.LastIndexOf("$$");
            bodyEnd = bodyEnd - endOfHeader;
            body = fullFile.Substring(endOfHeader, bodyEnd);

            return body;
        }

        private string GetFooter(string fullFile)
        {
            string footer;

            int bodyEnd = fullFile.LastIndexOf("$$");
            footer = fullFile.Substring(bodyEnd);

            return footer;
        }

        private List<Chord> BodyToChordList(string body)
        {
            List<Chord> chords = new List<Chord>();
            ChordDAO dao = new ChordDAO();

            string[] splitLines = body.Split(dao.LineDelim.ToCharArray());
            //getting a blank line at the end here
            foreach (string line in splitLines)
            {
                if (line.Trim(' ') != null && line != "")
                {
                    chords.Add(ChordFromLine(line));
                }
            }

            return chords;
        }

        private Chord ChordFromLine(string line)
        {
            Chord chord = new Chord();
            NoteList noteList = new NoteList();
            ChordDAO dao = new ChordDAO();

            string[] fieldArray = line.Split(dao.ValDelim.ToCharArray());
            string[] noteArray = fieldArray[1].Split(',');


            foreach (var item in noteList.NoteTree)
            {
                if (item.PrimaryName == fieldArray[0])
                {
                    chord.RootNote = item;
                }
                else if (item.SecondaryName == fieldArray[0])
                {
                    chord.RootNote = item;
                }
            }

            if (chord.RootNote == null)
            {
                throw new Exception("Error parsing data file: " + line);
            }

            chord.NoteDifference = GetNoteRelationships(fieldArray[1]);

            chord.ChordName = fieldArray[2];

            return chord;
        }

        private int[] GetNoteRelationships(string noteRelString)
        {
            string[] stringArray = noteRelString.Split(',');
            int[] noteRels = new int[stringArray.Length - 1];

            for (int i = 0; i < stringArray.Length - 1; i++)
            {
                int.TryParse(stringArray[i], out noteRels[i]);
            }

            return noteRels;
        }

        public List<Chord> CheckForInversions(Chord checkChord)
        {
            List<Chord> checkList = _allChords; //don't want to manipulate the cached chords

            foreach (Chord chord in checkList) //TODO: Looks like the chord here might not have a notelist?
            {
                if (!CheckInversion(checkChord, chord))
                {
                    checkList.Remove(chord);
                }

                if (!CheckInversion(chord, checkChord)) //reversing the order of chords should remove any false positives from multiple of the same notes
                {
                    checkList.Remove(chord);
                }
            }

            return checkList;
        }

        private bool CheckInversion(Chord chordOne, Chord chordTwo)
        {
            int i = 0;
            foreach (Note note in chordOne.ChordNoteList)
            {
                if (note.NoteExists(note, chordTwo.ChordNoteList))
                {
                    i++;
                }
            }

            if (i == chordOne.ChordNoteList.Count)
            {
                return true;
            }

            return false;
        }

        private void FileCheck(ChordDAO dao)
        {
            if (!File.Exists(dao.FilePath))
            {
                using (StreamWriter sw = File.CreateText(dao.FilePath))
                {

                }
            }
            //file exists, we don't need to create it
        }

        private bool FileEmpty(string fileContents)
        {

            if (fileContents.Count() >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        
    }
}
