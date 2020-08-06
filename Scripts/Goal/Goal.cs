using Godot;

public class Goal : Area2D {
    [Export]
    private bool isLeft;

    [Signal]
    public delegate void Score(bool isLeft);

    public override void _Ready() {
        float viewportWidth = this.GetViewport().GetVisibleRect().Size.x;
        float viewportHeight = this.GetViewport().GetVisibleRect().Size.y;

        RectangleShape2D collisionShape = (RectangleShape2D) this.GetNode<CollisionShape2D>("CollisionShape2D").Shape;

        float posX = this.isLeft ? 0 - collisionShape.Extents.x : viewportWidth + collisionShape.Extents.x ;
        this.Position = new Vector2(posX, viewportHeight / 2);
        
        collisionShape.Extents = new Vector2(collisionShape.Extents.x, viewportHeight);
    }

    public void OnBodyEntered(Node body) {
        this.EmitSignal("Score", this.isLeft);
    }
}
