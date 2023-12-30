using System;

public class Move {
  // Player this move belongs to
  public readonly Player owner;
  // Which TTT is this Move parented to
  private TTT parent;
  private bool parentSet = false;
  // The data of this move
  private string data;
  // Constructor
  public Move(Player owner){
    this.owner = owner;
    if(owner == null) {
      this.data = " ";
      return;
    }
    this.data = this.owner.team;
  }
  public Move(Player owner, TTT parent)
  : this(owner) {
    SetParent(parent);
  }
  public void SetParent(TTT parent) {
    if (parentSet) { return; }
    this.parent = parent;
    parentSet = true;
  }
  public TTT GetParent() {
    return parent;
  }
  public override string ToString(){
    return data;
  }
}