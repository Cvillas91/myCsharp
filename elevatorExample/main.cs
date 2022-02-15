/*
Name: Carlos Gonzalo Villas Velazquez
Date: Nov 10, 2020

Project #1

Details:
 
    You've been hired by a construction firm to help build the "brain" for a set of elevators in a new building. 
    Your task is to write the code that will control the elevators, and tell each elevator which floor to travel to next.

Building Description
    The building is 10 stories tall and the floors are numbered 0 - 10 inclusive. 
    The lobby is floor 0, and the penthouse is floor 10. The building contains one basement (floor -1).
    The building contains 2 elevators: A and B.
    Elevator A: Goes to all floors except the penthouse (floor 10).
    Elevator B: Goes all the way up (including 10) but does not go to the basement (-1).

Calling the Elevators
    The residents of the building can call the elevators by clicking the call buttons located next to the elevator shafts on their floor:
    Floors 1 - 9 contain two buttons to call the elevators: An "up" button and a "down" button.
    Floor 10 contains only a "down" button
    Floor -1 contains only an "up" button.

Riding In the Elevators
    Once inside of the elevators, a passenger can click the number of the floor that they wish to travel to.
    It takes each elevator 1 second to travel past each floor. For example: traveling from floor 0 to floor 4 would take 4 seconds.
    There is an emergency button inside each elevator. When that is pushed, the elevators go to their nearest floor and open their doors.
    The doors remain open until a reset button is pushed inside of the elevator.

Design Goals
    The goal of your code is to design a system that will get passengers from their starting floor, to their destination floor as 
    quickly as possible. The timer on each passenger starts the moment they request the elevator. 
    There are an unknown number of passengers in the building, on unknown floors, 
    and they will be requesting to go in random directions (up or down) to random floors, at random times.
    You can design this system in any number of ways (a library, a class, a set of event handlers or standalone functions, whatever). 
    It's up to you. Just make sure you document your code (either as comments or by including a Readme in your repository) so that the 
    elevator engineers know how to plug your "brain" code into the elevator's control logic.

Input
    You can expect that the other engineers are handling the user-interface part (the actual buttons). 
    But when those buttons are clicked, they need to be able to call the methods that you define. 
    So make sure your documentation explains what methods should be called when all the different buttons are pushed 
    (and the syntax they should use to call those methods).

Output
    If you think elevator A or B should take an action, you should print that action to the console. The actions available to you are:
    1. Move to a different floor
    2. Open doors
    3. Close doors
    So, for example: If you think elevator A should move to floor #5 and open it's doors you could log/print
    > A: move to 5
    > A: open doors

Extra Credit:
    Write a script that simulates 100 passengers requesting elevators at random times, from random floors, 
    and then requesting to go to random floors once they're inside the elevators. This script should execute over the course of 180 seconds. 
    While the script is running you should keep track of how long each passenger waits from the moment they request the elevator to the moment
    they get off at their destination. Use the Math.random() function to simulate the randomness.

*/

using System;

namespace Project1
{
    class Elevator { //Initial Class that contains the information of the elevator, as well as the methods for emergency break and reset.

        public int CurrentFloor;
        public bool doorsOpen = false;
        public bool toThePenthouse;
        public bool toTheLooby;
        public Elevator(int CurrentFloor, bool toThePenthouse, bool toTheLooby){ 
            this.CurrentFloor = CurrentFloor; 
            this.toThePenthouse = toThePenthouse; 
            this.toTheLooby = toTheLooby; 
        } 

        public void EmergencyBreak(){
            Console.WriteLine("Open Doors");
            doorsOpen = true;
        }

        public void Reset(){
            Console.WriteLine("Close Doors");
            doorsOpen = false;
        }

    }


    class Program
    {
        static void Main(string[] args)
        {
            
            var rand = new Random(); //Initializes random values
            Elevator ElevatorA = new Elevator(9, true, false); //Initializes elevator A to floor #9
            Elevator ElevatorB = new Elevator(1, false, true); //Initializes elevator B to floor #1
            int totalTime = 0;

            for (int i = 1; i < 101; i++){
                int randomFloorTo = rand.Next(0,11);
                int randomFloorFrom = rand.Next(0,11);
                //Console.WriteLine("Elevator A: " + ElevatorA.CurrentFloor.ToString());
                //Console.WriteLine("Elevator B: " + ElevatorB.CurrentFloor.ToString());
                Console.WriteLine("Person: " + i.ToString() + " going from: " + randomFloorFrom.ToString() + " to: " + randomFloorTo.ToString());
                if (randomFloorTo == randomFloorFrom){ //Case if the person is already on the same floor
                    Console.WriteLine("No need for elevator, person is already on the same floor!");
                }else if ((randomFloorFrom == 0 && randomFloorTo == 10) || (randomFloorFrom == 10 && randomFloorTo == 0)){
                    Console.WriteLine("Person can't go to that floor!");
                }else if (randomFloorTo == 10){ //Case if the person wants to go to the Penthouse
                    Console.WriteLine("Elevator A is on the floor: " + ElevatorA.CurrentFloor.ToString() + ", estimated time to arrival: " + Math.Abs(ElevatorA.CurrentFloor-randomFloorFrom) + " seconds");
                    Console.WriteLine("Closing doors... Elevator A is moving to floor: " + randomFloorFrom.ToString());
                    Console.WriteLine("Elevator A has reached floor " + randomFloorFrom.ToString() + ", Opening Doors....");
                    Console.WriteLine("Closing doors... Elevator A is moving to floor: " +  randomFloorTo.ToString());
                    Console.WriteLine("Elevator A has reached floor " + randomFloorTo.ToString() + ", Opening Doors....");
                    ElevatorA.CurrentFloor = randomFloorTo;
                    totalTime += Math.Abs(ElevatorA.CurrentFloor-randomFloorFrom) + Math.Abs(randomFloorFrom - randomFloorTo);
                } else if (randomFloorTo == 0){ //Case if the person wants to go to the Lobby
                    Console.WriteLine("Elevator B is on the floor: " + ElevatorB.CurrentFloor.ToString()+ ", estimated time to arrival: " + Math.Abs(ElevatorB.CurrentFloor-randomFloorFrom) + " seconds");
                    Console.WriteLine("Closing doors... Elevator B is moving to floor: " + randomFloorFrom.ToString());
                    Console.WriteLine("Elevator B has reached floor " + randomFloorFrom.ToString() + ", Opening Doors....");
                    Console.WriteLine("Closing doors... Elevator B is moving to floor: " +  randomFloorTo.ToString());
                    Console.WriteLine("Elevator B has reached floor " + randomFloorTo.ToString() + ", Opening Doors....");
                    ElevatorB.CurrentFloor = randomFloorTo;
                    totalTime += Math.Abs(ElevatorB.CurrentFloor-randomFloorFrom) + Math.Abs(randomFloorFrom - randomFloorTo);
                } else if (Math.Abs(ElevatorA.CurrentFloor - randomFloorFrom) <= Math.Abs(ElevatorB.CurrentFloor - randomFloorFrom) ) { //Case if the elevator A is closer to person floor
                    Console.WriteLine("Elevator A is on the floor: " + ElevatorA.CurrentFloor.ToString()+ ", estimated time to arrival: " + Math.Abs(ElevatorA.CurrentFloor-randomFloorFrom) + " seconds");
                    Console.WriteLine("Closing doors... Elevator A is moving to floor: " + randomFloorFrom.ToString());
                    Console.WriteLine("Elevator A has reached floor " + randomFloorFrom.ToString() + ", Opening Doors....");
                    Console.WriteLine("Closing doors... Elevator A is moving to floor: " +  randomFloorTo.ToString());
                    Console.WriteLine("Elevator A has reached floor " + randomFloorTo.ToString() + ", Opening Doors....");
                    ElevatorA.CurrentFloor = randomFloorTo;
                    totalTime += Math.Abs(ElevatorA.CurrentFloor - randomFloorFrom) + Math.Abs(randomFloorFrom - randomFloorTo);
                } else { //Case if the elevator B is closer to person floor
                    Console.WriteLine("Elevator B is on the floor: " + ElevatorB.CurrentFloor.ToString()+ ", estimated time to arrival: " + Math.Abs(ElevatorB.CurrentFloor-randomFloorFrom) + " seconds");
                    Console.WriteLine("Closing doors... Elevator B is moving to floor: " + randomFloorFrom.ToString());
                    Console.WriteLine("Elevator B has reached floor " + randomFloorFrom.ToString() + ", Opening Doors....");
                    Console.WriteLine("Closing doors... Elevator B is moving to floor: " +  randomFloorTo.ToString());
                    Console.WriteLine("Elevator B has reached floor " + randomFloorTo.ToString() + ", Opening Doors....");
                    ElevatorB.CurrentFloor = randomFloorTo;
                    totalTime += Math.Abs(ElevatorB.CurrentFloor - randomFloorFrom) + Math.Abs(randomFloorFrom - randomFloorTo);
                }

            }

             Console.WriteLine("The total time it took to move the 100 persons was: " + totalTime.ToString() + " seconds");

        }
    }
}
