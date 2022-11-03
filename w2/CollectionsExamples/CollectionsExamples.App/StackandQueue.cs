namespace CollectionsExamples.App
{
    public class StackAndQueue
    {
        //first in last out

        //clear() remove all
        //bool contains(obj) checks if obj is present
        //obj peek() returns obj at top
        //obj pop() retuns AND REMOVES obj at top
         //void push(obj) adds obj to top
         //object[] ToArrary(); copies stack to new arrary 
        public Stack<DateTime> stack;

        //first in first out
          
          int size = 100;

        public Queue<TimeSpan> queue;
        public StackAndQueue()
        {   
            stack = new Stack<DateTime>();
            queue = new Queue<TimeSpan>();
            DateTime time = new DateTime();

            for (int i=0; i<size; i++)
            {
                 stack.Push(DateTime.Now);

            }
             TimeSpan span =new TimeSpan();
            for (int i= 0; i<size-1;i++)
            {
                
              queue.Enqueue(span);
            }

            for(int i=0; i< queue.Count;i++){
                Console.WriteLine(queue.Dequeue());
            }




        }
    }
}