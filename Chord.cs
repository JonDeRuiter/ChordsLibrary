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
        private List<Chord> ChordDataste = new List<Chord>();

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

        public void ParseChord()
        {

        }

        //Insert Chord if new
        public void InsertChord(Chord chord)
        {
            chord.NoteDifference = FindNoteRelationship(chord.ChordNoteList);
            List<Chord> chords = GenAllChordsFromEntry(chord);
            ChordDTO.InsertNewChord(chords);           
        }
        
        //the mathematical relationship between the root note and the rest of the notes in the chord define their chord. This method calculates those relationships
        private static int[] FindNoteRelationship(List<Note> noteNames)
        {
            //Need to check this to make sure it functions as expected - [0] should always hold a value of '0'
            int[] noteRel = new int[(noteNames.Count)];

            //rootNote = 3 for C
            int rootNote = noteNames[0].Index;
     
            for (int i = 0; i < noteRel.Length; i++)
            {
                //int safeNote = Note.FindNoteIncrement(noteNames[0], noteNames[i]);
                //noteRel[(i - 1)] = safeNote - rootNote;
                noteRel[i] = Note.FindNoteIncrement(noteNames[0], noteNames[i]);

            }

            return noteRel;
        }
        
        private List<Chord> GenAllChordsFromEntry(Chord origChord)
        {
            List<Chord> allRoots = new List<Chord>();
            NoteList noteList = new NoteList();

            for (int i = 0; i < 12; i++)
            {
                Chord chord = new Chord(noteList.NoteTree[i], origChord.NoteDifference, ParseChordName(origChord.ChordName, i));
                chord.ChordNoteList = GetNoteListFromDefinition(chord.RootNote, chord.NoteDifference, noteList);
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

        private List<Note> GetNoteListFromDefinition(Note rootNote, int[] noteDifference, NoteList noteList)
        {
            List<Note> foundNotes = new List<Note>();

            foundNotes.Add(rootNote);

            for (int i = 1; i < noteDifference.Length; i++)
            {
                foundNotes.Add(GetNoteFromRelationship(rootNote, noteDifference[i], noteList));
            }

            return foundNotes;
        }

        private Note GetNoteFromRelationship(Note rootNote, int nextRel, NoteList noteList)
        {
            Note nextNote = rootNote;
            
            for (int i = 0; i < nextRel; i++)
            {
                nextNote = noteList.Next(nextNote);
            }

            return nextNote;
        }
    }
}
