using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ChordsLibrary.DataAccess
{
    class ChordDTO
    {
        //these can be identical between DTO and DAO

        private string filePath;
        private List<string> body;
        private Char[] lineDelim;
        private Char[] valDelim;
        private string header;
        private string footer;



        public string Header { get { return header; } set { this.header = value; } }
        public string Footer { get { return footer; } set { this.footer = value; } }
        public  Char[] ValDelim
        {
            get { return valDelim; }
            set => valDelim[0] = '|';
        }
        public Char[] LineDelim
        {
            get { return lineDelim; }
            set => lineDelim[0] = '*';
        }
        public List<string> Body { get; set; }
        public string FilePath
        {
            get { return filePath; }
            set => filePath = value + ".txt";
            //set => filePath = "~\\ChordLibrary\\Storage\\" + value + ".txt";
        }

        public ChordDTO()
        {

        }
    
        public static string InsertNewChord(List<Chord> chords)
        {
            //TODO: C Major Triad getting here starts with Bsharp, and has no notes
            Chord newChord = chords[0];
            ChordDTO newInsert = new ChordDTO();
            string chordLength = (newChord.NoteList.Count).ToString();

            var enviroment = System.Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(enviroment).Parent.FullName;
            projectDirectory += "\\Storage\\" + chordLength;

            newInsert.FilePath = projectDirectory;

            FileCheck(newInsert);
            
            string fullFile = BuildBody(chords);

            string[] lines = fullFile.Split(newInsert.LineDelim);
            int lineCount = lines.Length;
            

            fullFile = BuildHeader(lineCount.ToString(), chordLength) + fullFile;
            fullFile = fullFile + BuildFooter(lineCount.ToString(), chordLength);

            //write to file
            WriteFile(newInsert, fullFile);

            return "New Chord Added";
        }
        
        private static string BuildHeader(string numLines, string chordLength)
        {
            string returnString;
            DateTime lastMod = new DateTime();
            string lastModDt = lastMod.ToLongDateString();
            returnString = "$Header: File for Chords of length {chordLength} contains {numLines} records. Last updated {lastModDt} /n CSV format with RootNote, NoteDifference, ChordName**";
            return  returnString;
        }

        private static string BuildFooter(string numLines, string chordLength)
        {
            string returnString;
            DateTime lastMod = new DateTime();
            string lastModDt = lastMod.ToLongDateString();
            returnString = "**$Footer: File for Chords of length {chordLength} wrote {numLines} records. Last updated {lastModDt}";
            return returnString;
        }

        private static string BuildBody(List<Chord> chords)
        {
            List<Chord> allChords = new List<Chord>();
            //Access data add to list
            Chord newChord = chords[0];
            int chordLength = newChord.NoteDifference.Length;
            allChords = ChordDAO.GetAllChordData(chordLength.ToString());

            //add new data
            foreach (Chord c in chords)
            {
                allChords.Add(c);
            }            
            //sort data - do I actually care how?
            //TODO: figure out of sorting helps at all.
            allChords.Sort();

            string fileBody = ChordsToBody(allChords);

            return fileBody;
        }        

        private static string ChordsToBody(List<Chord> chordList)
        {
            string body = "";
            foreach (Chord chord in chordList)
            {
                body += ChordToLine(chord);
            }
            return body;
        }

        private static string ChordToLine(Chord chord)
        {
            ChordDTO dto = new ChordDTO();
            string line = chord.RootNote.ToString() + dto.ValDelim[0];

            foreach (int i in chord.NoteDifference)
            {
                //just combining notes into a string doesn't let extract them
                line += chord.NoteDifference[i] + ',';
            }

            line += chord.ChordName + dto.LineDelim;
            
            return line;

        }

        private static void FileCheck(ChordDTO dto)
        {
            if (!File.Exists(dto.FilePath))
            {
                using(StreamWriter sw  = File.CreateText(dto.FilePath))
                {
                    //this creates a file we can now use
                }
            }            
                //file exists, we don't need to create it            
        }

        private static void WriteFile(ChordDTO dto, string fullFile)
        {
            using (StreamWriter sw = File.CreateText(dto.FilePath))
            {
                sw.Write(fullFile);
            }
        }

    }
}
