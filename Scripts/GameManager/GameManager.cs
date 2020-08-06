using Godot;
using System.Collections.Generic;

public class GameManager : Node2D {
    private int scoreLeft;
    private int scoreRight;

    private List<Pallete> palletes;
    private Ball ball;

    public override void _Ready() {
        this.scoreLeft = 0;
        this.scoreRight = 0;

        foreach (Goal goal in this.GetNode<Node2D>("Goals").GetChildren())
            goal.Connect("Score", this, "On_GoalScore");

        this.palletes = new List<Pallete>();

        foreach (Pallete pallete in this.GetNode<Node2D>("Players").GetChildren())
            this.palletes.Add(pallete);

        this.ball = this.GetNode<Ball>("Ball");
    }

    private void On_GoalScore(bool isLeft) {
        if (isLeft)
            this.scoreRight++;
        else
            this.scoreLeft++;

        GD.Print("Left Score: " + this.scoreLeft);
        GD.Print("Right Score: " + this.scoreRight);

        foreach (Pallete pallete in this.palletes)
            pallete.SetInitialPosition();

        this.ball.SetInitialPosition();
    }
}
