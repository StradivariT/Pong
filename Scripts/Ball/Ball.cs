using Godot;

public class Ball : KinematicBody2D {
    [Export]
    private float speedX;

    [Export]
    private float speedY;

    [Export]
    private float initialAngle;

    [Export]
    private float increment;

    private float movementX;
    private float movementY;

    private bool startMoving;

    private RandomNumberGenerator rng;

    public override void _Ready() {
        this.SetInitialPosition();

        this.rng = new RandomNumberGenerator();
    }

    public override void _UnhandledKeyInput(InputEventKey @event) {
        if (@event.IsActionPressed("Start") && !this.startMoving) {
            this.StartMovement();
            this.GetTree().SetInputAsHandled();
        }
    }

    public override void _PhysicsProcess(float delta) {
        if (!this.startMoving)
            return;

        KinematicCollision2D collision = this.MoveAndCollide(new Vector2(this.movementX * delta, this.movementY * delta));

        if (collision == null) 
            return;

        string collideObjectName = collision.Collider.Get("name").ToString();

        if (collideObjectName.Contains("Pallete")) {
            this.movementX *= -1;
        
            if (this.movementX < 0)
                this.movementX -= this.increment;
            else
                this.movementX += this.increment;

            if (this.movementY < 0)
                this.movementY -= this.increment;
            else
                this.movementY += this.increment;
        } else
             this.movementY *= -1;
    }

    public void SetInitialPosition() {
        float viewportWidth = this.GetViewport().GetVisibleRect().Size.x;
        float viewportHeight = this.GetViewport().GetVisibleRect().Size.y;

        this.Position = new Vector2(viewportWidth / 2, viewportHeight / 2);

        this.startMoving = false;
    }

    private void StartMovement() {
        this.rng.Randomize();

        this.movementX = this.speedX * (this.rng.RandiRange(-1000, 1000) > 0 ? 1 : -1);
        this.movementY = this.speedY * (this.rng.RandiRange(-1000, 1000) > 0 ? 1 : -1);

        float angle = this.rng.RandfRange(0, this.initialAngle);
        float angleRadians = angle * Mathf.Pi / 180;

        this.movementY *= Mathf.Sin(angleRadians);

        this.startMoving = true;
    }
}
