using Godot;

public class Boundary : StaticBody2D {
    [Export]
    private bool isUpper;

    public override void _Ready() {
        float viewportWidth = this.GetViewport().GetVisibleRect().Size.x;
        float viewportHeight = this.GetViewport().GetVisibleRect().Size.y;

        RectangleShape2D collisionShape = (RectangleShape2D) this.GetNode<CollisionShape2D>("CollisionShape2D").Shape;

        float posY = this.isUpper ? -collisionShape.Extents.y : viewportHeight + collisionShape.Extents.y;
        this.Position = new Vector2(viewportWidth / 2, posY);

        collisionShape.Extents = new Vector2(viewportWidth, collisionShape.Extents.y);
    }
}
