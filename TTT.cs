// redo this system so everything starts at 0, it can be adjusted when we print but everything should be in terms of 0 so there isn't any confusion
using System;

public class TTT {
  private Object[,] ttt = {{" ", " ", " "}, {" ", " ", " "}, {" ", " ", " "}};
  private int count;
  private TTT parent = null;
  private bool winnerCheck = false;
  private string _winner = null;
  public string Winner{
    get {
      _winner = evaluateWinner();
      return _winner;
    }
  }
  public TTT()
  : this(1) 
  {
  }
  // constructor
  public TTT(int count) {
    // set this.count to count
    this.count = count;
    // if count is 1, fill it with blank moves
    if(count == 1) {
      for(int i = 0; i < 3; i++) {
        for(int j = 0; j < 3; j++) {
          this.ttt[i,j] = new Move(null, this);
        }
      }
      // if count isn't 1, then fill it with TTT's with count - 1
    } else {
      for(int i = 0; i < 3; i++) {
        for(int j = 0; j < 3; j++) {
          this.ttt[i,j] = new TTT(count - 1, this);
        }
      }
    }
  }
  // constructor w/ parent
  public TTT(int count, TTT parent)
    // do "base logic"
    : this(count) {
      // set this.parent to parent
      this.parent = parent;
    }
  
  // move, only if we are in the inmost TTT
  public void move(Move move, int x, int y) {
    // if we're in the inmost TTT move, and we're done
    if(count == 1) {
      move.SetParent(this);
      this.ttt[x-1, y-1] = move;
      evaluateWinner();
      return;
    }
    // if we're not then go deeper and call move recursively
    Move referenceMove = ((Move) referencing(x, y));
    TTT referenceTTT = referenceMove.GetParent();
    for(int i = 0; i < 3; i++) {
      for(int j = 0; j < 3; j++) {
        if(referenceTTT.ObjectAt(i, j).Equals(referenceMove)) {
          referenceTTT.move(move, i+1, j+1);
          return;
        }
      }
    }
  }
  // simple print method
  public void print() {
    // Console.WriteLine("simple print:");
    for(int i = 0; i < 3; i++) {
      for(int j = 0; j < 3; j++) {
          Console.Write("[{0}]", ttt[i,j].ToString());
      }
      Console.WriteLine();
    }
  }
  // more in depth print method
  public void visualize(){
    // Console.WriteLine("advanced print:");
    // a reference object so we can go deeper into the TTT object
    Object reference = this;
    // the amount of spaces per side according to count (3 ^ count)
    int spaces = (int) Math.Pow(3, count);
    // Console.WriteLine("count: {0} spaces: {1}", count, spaces);
     // numbers
    Console.Write("  ");
    for(int i = 0; i < spaces; i++){
      Console.Write("{0,3}", i+1);
    }
    Console.WriteLine();
    // finally print out everything in a spaces * spaces grid
    for(int i = 0; i < spaces; i++) {
      Console.Write("{0,3}",i+1);
      for(int j = 0; j < spaces; j++){
        Console.Write("[{0}]", referencing(i+1, j+1).ToString());
      }
      Console.WriteLine();
    }
  }
  // more advanced visualization
  public void advanced(){
    Object reference = this;
    int spaces = (int) Math.Pow(3, count);
    for(int i = 0; i < spaces; i++){
      for(int j = 0; j < spaces; j++){
        if(j == spaces-1){
          Console.Write("{0}", referencing(i+1, j+1).ToString());
        } else {
          Console.Write("{0}|", referencing(i+1, j+1).ToString());
        }
      }
      Console.WriteLine();
      // print out the divider
      
      if((i+1)%3 == 0 && i != 0 && i != spaces-1){
        for(int j = 0; j < spaces/3; j++){
          Console.Write("-----+");
        }
      } else {
        for(int j = 0; j < spaces/3; j++){
          Console.Write("-+-+-|");
        }
      }
      Console.WriteLine();
    }
  }
  public Object referencing(int x, int y) {
    // return object giving element at [x,y] according to (1->spaces) * (1->spaces)
    Object returnObject = this;
    // the amount of spaces and pool size
    int spaces = (int) Math.Pow(3, count);
    int poolSize = spaces / 3;
    // a count variable that we can decrement without changing the starting value
    int _count = count - 1;
    // do this loop according to how many TTTs are in TTTs (count)
    for(int i = 0; i < count; i++) {
      // temp variables _x and _y to play with x and y
      int _x = x;
      int _y = y;
      // get the most immediate TTT location coordinates (uses round-up int division)
      // z = (x + y - 1) / y
      for(int j = 0; j < _count; j++) {
        _x = (_x + 2) / 3;
        _y = (_y + 2) / 3;
      }
      // set returnObject to general area coords, if it's a TTT
      if(Object.ReferenceEquals(returnObject.GetType(), new TTT(1).GetType())){
        returnObject = ((TTT)returnObject).ObjectAt(_x-1, _y-1);
      }
      // Adjust x and y according to the number of poolSize
      x = x % poolSize;
      y = y % poolSize;
      // if x or y divides into poolSize, then the coord = poolSize
      if(x == 0) { x = poolSize; }
      if(y == 0) { y = poolSize; }
      // decrement _count to let us know we are deeper into this.TTT object + adjust poolSize
      _count--;
      poolSize /= 3;
    }
    // return returnObject
    return returnObject;
  }
  public void advancedPrint(){

    if(count != 1) {
      ((TTT)this.ttt[0,0]).advancedPrint();
      return;
    }
  }
  // gets the count of this TTT
  public int GetCount(){
    return this.count;
  }
  // returns the TTT at [i,j], if one exists
  public TTT TTTAt(int i, int j) {
    if(count == 1) {
      return this;
    }
    return ((TTT) this.ttt[i,j]);
  }
  // checks the object at the specific [i,j]
  public Object ObjectAt(int i, int j) {
    return this.ttt[i,j];
  }
  // gets the parent of this ttt
  public TTT GetParent(){
    return this.parent;
  }
  // evaluate a winner
  // BUG there's something wrong with winning horizontally on the middle row+winning vertically middle/right collumn
  private string evaluateWinner() {
    winnerCheck = false;
    _winner = checkWinner(ttt[0, 0], ttt[0, 1], ttt[0, 2]);
    _winner = checkWinner(ttt[1, 0], ttt[1, 1], ttt[1, 2]);
    _winner = checkWinner(ttt[2, 0], ttt[2, 1], ttt[2, 2]);
    _winner = checkWinner(ttt[0, 0], ttt[1, 0], ttt[2, 0]);
    _winner = checkWinner(ttt[0, 1], ttt[1, 1], ttt[2, 1]);
    _winner = checkWinner(ttt[0, 2], ttt[1, 2], ttt[2, 2]);
    _winner = checkWinner(ttt[0, 0], ttt[1, 1], ttt[2, 2]);
    _winner = checkWinner(ttt[0, 2], ttt[1, 1], ttt[2, 0]);

    return _winner;
  }
  // winner helper method
  private String checkWinner(Object a, Object b, Object c){
    if(winnerCheck){
      return _winner;
    }
    if(Convert.ToString(a).Equals(Convert.ToString(b)) && Convert.ToString(b).Equals(Convert.ToString(c)) && !Convert.ToString(a).Equals(" ")) {
      winnerCheck = true;
      return Convert.ToString(a);
    }
    return " ";
  }
  // ignore this for now, it's useful for row-major order
  private int encode(int i, int j) {
    return (3 * i) + j;
  }
  // returns overall winner when printed
  public override string ToString(){
    return Winner;
  }
}
