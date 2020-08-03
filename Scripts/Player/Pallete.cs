using Godot;

public class Pallete : Sprite {
    [Export]
    private float horizontalPadding;

    [Export]
    private bool isLeft;

    [Export]
    private float speed;

    private float yOffset;
    private float viewportHeight;

    private bool readyToClearInputs;

    private bool upPressed;
    private bool downPressed;

    private string side;

    public override void _Ready() {
        float viewportWidth = this.GetViewport().GetVisibleRect().Size.x;
        this.viewportHeight = this.GetViewport().GetVisibleRect().Size.y;

        float xPosition = this.isLeft ? this.horizontalPadding : viewportWidth - this.horizontalPadding;

        this.Position = new Vector2(xPosition, this.viewportHeight / 2);

        this.side = this.isLeft ? "Left" : "Right";

        this.yOffset = this.Texture.GetSize().y / 2;
    }

    public override void _Process(float delta) {
        this.ClearInputs();

        this.upPressed = this.upPressed || Input.IsActionPressed("Up" + this.side);
        this.downPressed = this.downPressed || Input.IsActionPressed("Down" + this.side);
    }

    private void ClearInputs() {
        if (!this.readyToClearInputs)
            return;

        this.upPressed = false;
        this.downPressed = false;

        this.readyToClearInputs = false;
    }

    public override void _PhysicsProcess(float delta) {
        this.readyToClearInputs = true;

        float move = 0;

        if (this.upPressed)
            move = -this.speed;
        else if (this.downPressed)
            move = this.speed;

        float newY = this.Position.y + move * delta;
        
        if (newY - this.yOffset <= 0) 
            newY = this.yOffset;
        else if (newY + this.yOffset >= this.viewportHeight)
            newY = this.viewportHeight - this.yOffset;

        this.Position = new Vector2(this.Position.x, newY);
    }
}
