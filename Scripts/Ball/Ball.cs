using Godot;

public class Ball : KinematicBody2D {
    [Export]
    private float speedX;

    [Export]
    private float speedY;

    [Export]
    private float initialAngle;

    private float movementY;

    public override void _Ready() {
        float viewportWidth = this.GetViewport().GetVisibleRect().Size.x;
        float viewportHeight = this.GetViewport().GetVisibleRect().Size.y;

        this.Position = new Vector2(viewportWidth / 2, viewportHeight / 2);

        float angleRadians = this.initialAngle * Mathf.Pi / 180;
        this.movementY = this.speedY * Mathf.Sin(angleRadians);
    }

    public override void _PhysicsProcess(float delta) {
        KinematicCollision2D collision = this.MoveAndCollide(new Vector2(this.speedX * delta, this.movementY * delta));

        if (collision == null) 
            return;

        string collideObjectName = collision.Collider.Get("name").ToString();

        if (collideObjectName.Contains("Pallete"))
            this.speedX *= -1;
        else
            this.movementY *= -1;
    }
}
