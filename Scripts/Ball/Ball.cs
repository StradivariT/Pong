using Godot;

public class Ball : Sprite {
    [Export]
    private float speedX;

    [Export]
    private float speedY;

    [Export]
    private float initialAngleRange;

    private float yMovement;

    public override void _Ready() {
        float viewportWidth = this.GetViewport().GetVisibleRect().Size.x;
        float viewportHeight = this.GetViewport().GetVisibleRect().Size.y;

        this.initialAngleRange = this.initialAngleRange * Mathf.Pi / 180;

        this.Position = new Vector2(viewportWidth / 2, viewportHeight / 2);
        this.yMovement = this.speedY * Mathf.Sin(this.initialAngleRange);
    }

    public override void _PhysicsProcess(float delta) {
        this.Position = new Vector2(this.Position.x + this.speedX * delta, this.Position.y + this.yMovement * delta);
    }


}