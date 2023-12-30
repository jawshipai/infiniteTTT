using System;
using System.Collections;

class Program {
  public static void Main (string[] args) {
    Console.WriteLine("TTT:");
    // Players Josh and Silas
    Player josh = new Player("Josh", "x");
    Player silas = new Player("Silas", "o");

    ArrayList players = new ArrayList();
    players.Add(josh);
    players.Add(silas);
    
    // Tic Tac Toe Object "ttt"
    TTT ttt = new TTT(3);

    
    bool winnerFound = false;
    while(!winnerFound){
      ttt.print();
      ttt.advanced();
      Player turnholder;
      Console.Write("i: ");
      string input1 = Console.ReadLine();
      Console.Write("j: ");
      string input2 = Console.ReadLine();
      Console.Write("who: ");
      string input3 = Console.ReadLine();
      
      int coord1 = Convert.ToInt32(input1);
      int coord2 = Convert.ToInt32(input2);
      turnholder = (Player) players[Convert.ToInt32(input3)-1];
      
      ttt.move(new Move(turnholder), coord1, coord2);
      Console.WriteLine("winner:"+ttt.ToString());
    }
  }
}




/*
    // Moving using a "select" approach
    TTT select;
    select = ttt.TTTAt(0,0);
    select.move(new Move(josh), 1, 1);
    select.move(new Move(josh), 1, 2);
    select = ttt.TTTAt(1,1);
    select.move(new Move(josh), 1, 3);
    select = ttt.TTTAt(2,0);
    select.move(new Move(josh), 1, 1);
    select.move(new Move(josh), 2, 2);
    select.move(new Move(josh), 3, 3);
    // Moving using the move method straight up
    ttt.move(new Move(mike), 1, 3);
    ttt.move(new Move(mike), 5, 6);
    ttt.move(new Move(mike), 9, 7);

    ttt.move(new Move(josh), 9, 9);

    ttt.move(new Move(mike), 2, 2);
    */