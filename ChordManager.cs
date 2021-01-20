using ChordsLibrary.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChordsLibrary
{
    class ChordManager
    {
        //This is where I'm going to manage caching various information for later use and managing in IO for this library
        private ChordDAO dao = new ChordDAO();
        private ChordDTO dta = new ChordDTO();
        private Chord foundChord = new Chord();
        private Chord unknownChord = new Chord();
        private List<Chord> chordList = new List<Chord>();
        private string message;

        public Chord FindChord(List<Note> notesEntered)
        {            
            unknownChord.ChordNoteList = notesEntered;

            if (notesEntered.Count == 0)
            {
                message = "Notes are empty";
            }
            else
            {
                unknownChord.RootNote = notesEntered[0];

                unknownChord.NoteDifference = FindNoteRelationship(notesEntered);
                                
                if (unknownChord.ChordName == null)
                {
                    unknownChord = dao.FindAChord(unknownChord);
                    message = "We found your chord!";
                }
                else
                {
                    message = "Either this is not a formal chord, or we don't have it in our system yet.";
                }
            }
            return unknownChord;
        }

        public string InsertNewChord(Chord newChord)
        {
            if (IsNewChord(newChord))
            {
                unknownChord.InsertChord(newChord);            
                return "Chord added";
            }
            else
            {
                return "There was a problam inserting that chord.";
            }
        }


        private bool IsNewChord(Chord chord)
        {           
            chord.RootNote = chord.ChordNoteList[0];
            ParseChord(chord);
            chord.NoteDifference = chord.FindNoteRelationship(chord.ChordNoteList);
            Chord emptyChord = dao.FindAChord(chord);

            //I just picked an element that will be there everytime
            if (emptyChord.ChordName == null)
            {
                return true;
            }

            return false;
        }

    }
}
