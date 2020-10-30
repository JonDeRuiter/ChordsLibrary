using ChordsLibrary.DataAccess;
using System;
using System.Collections.Generic;

namespace ChordsLibrary
{
    public class Chord
    {
        public string ChordName { get; set; }
        public Note RootNote { get; set; }
        public List<Note> ChordNoteList { get; set; }
        public int[] NoteDifference { get; set; }
        public string Message { get; set; }


        public Chord()
        {

        }

        public Chord(string chordName, List<Note> notes)
        {
            ChordName = chordName;
            ChordNoteList = notes;
        }

        public Chord(Note rootNote, int[] noteDifference, string chordName)
        {
            RootNote = rootNote;
            ChordName = chordName;
            NoteDifference = noteDifference;
        }

        //Insert Chord if new
        public string InsertChord(Chord chord)
        {
            if (chord.IsNewChord(chord))
            {
                //insert chord + all variations of it root c through b
                //TODO: Figure out how to get chords with the effective same root note to map the the correct one - IE C instead of B Sharp
                //now just check to make sure that works
                List<Chord> chords = GenAllChordsFromEntry(chord);
                ChordDTO.InsertNewChord(chords);
            }
            else
            {
                //Don't insert chord
                return "Chord already exists.";
            }

            return "Chord saved successfuly";
        }

        public static Chord FindChord(List<Note> notesEntered)
        {
            Chord unknownChord = new Chord();
            unknownChord.ChordNoteList = notesEntered;
            unknownChord.RootNote = notesEntered[0];

            unknownChord.NoteDifference = FindNoteRelationship(notesEntered);

            if (unknownChord.IsNewChord(unknownChord))
            {
                unknownChord.Message = "Either this is not a formal chord, or we don't have it in our system yet.";
            }
            else
            {
                unknownChord = ChordDAO.FindAChord(unknownChord);
                unknownChord.Message = "We found your chord!";
            }

            return unknownChord;
        }

        private bool IsNewChord(Chord chord)
        {
            Chord emptyChord = ChordDAO.FindAChord(chord);

            //I just picked an element that will be there everytime
            if (emptyChord.ChordName == null)
            {
                return true;
            }

            return false;
        }

        //the mathematical relationship between the root note and the rest of the notes in the chord define their chord. This method calculates those relationships
        private static int[] FindNoteRelationship(List<Note> noteNames)
        {
            //Need to check this to make sure it functions as expected - [0] should always hold a value of '0'
            int[] noteRel = new int[(noteNames.Count - 1)];


            int rootNote = noteNames[0].Index;
     
            for (int i = 0; i < noteRel.Length; i++)
            {
                int safeNote = Note.FindNoteIncrement(noteNames[0], noteNames[i]);
                noteRel[(i - 1)] = safeNote - rootNote;
            }

            return noteRel;
        }
        
        private List<Chord> GenAllChordsFromEntry(Chord origChord)
        {
            List<Chord> allRoots = new List<Chord>();
            NoteList noteList = new NoteList();

            for (int i = 1; i < 13; i++)
            {
                Chord chord = new Chord(noteList.NoteTree[i], origChord.NoteDifference, origChord.ChordName);
                allRoots.Add(chord);
            }

            return allRoots;
        }

        private string ParseChordName(string sbmtdName, int i)
        {
            string parsedName;
            NoteList noteList = new NoteList();
            Note note = noteList.NoteTree[i];
            int space = sbmtdName.IndexOf(" "); //First set of characters should be the root note
            parsedName = (note.PrimaryName + sbmtdName.Substring(space));

            return parsedName;
        }


    }
}
