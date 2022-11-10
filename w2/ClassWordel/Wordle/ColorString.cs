using System;

namespace Wordle {
    public class ColorString{
        //Fields
        string S {get;set;}
        static int size=5;
        ConsoleColor[] bgs = new ConsoleColor[size];

        //Constructors
        public ColorString(){
            for (int i=0;i<size;i++){
                bgs[i]=ConsoleColor.Gray;
            }
        }

        //Methods

        public void setCharColor(ConsoleColor concol, int index){
            //
            bgs[index] = concol;
        }
        public void printS(){
            for(int i=0;i<size;i++){
                Console.BackgroundColor=bgs[i];
                Console.Write("{0}",S[i]);
            }
            //reset console color to black after printing
            Console.BackgroundColor=ConsoleColor.Black;
        }
    }
}