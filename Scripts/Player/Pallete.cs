using Godot;

public class Pallete : KinematicBody2D {
    [Export]
    private float horizontalPadding;

    [Export]
    private bool isLeft;

    [Export]
    private float speed;

    private string side;

    private bool isReadyToClearInputs;
    private bool upPressed;
    private bool downPressed;

    public override void _Ready() {
        this.SetInitialPosition();

        this.side = this.isLeft ? "Left" : "Right";
    }

    public override void _Process(float delta) {
        this.ClearInputs();

        this.upPressed = this.upPressed || Input.IsActionPressed("Up" + this.side);
        this.downPressed = this.downPressed || Input.IsActionPressed("Down" + this.side);
    }

    private void ClearInputs() {
        if (!this.isReadyToClearInputs)
            return;

        this.upPressed = false;
        this.downPressed = false;

        this.isReadyToClearInputs = false;
    }

    public override void _PhysicsProcess(float delta) {
        if (this.upPressed)
            this.MoveAndSlide(new Vector2(0, -this.speed));
        else if (this.downPressed)
            this.MoveAndSlide(new Vector2(0, this.speed));

        this.isReadyToClearInputs = true;
    }

    public void SetInitialPosition() {
        float viewportWidth = this.GetViewport().GetVisibleRect().Size.x;
        float viewportHeight = this.GetViewport().GetVisibleRect().Size.y;

        float posX = this.isLeft ? this.horizontalPadding : viewportWidth - this.horizontalPadding;

        this.Position = new Vector2(posX, viewportHeight / 2);
    }
}