using UnityEngine;

public interface ITouchController
{
    void tap(Vector2 position);

    void drag(Vector2 current_position);

    void dragEnd();

    void pinch(float startDist, float endDist, float relative_distance);
    void pinchEnded();
    void rotate(float new_angle);
    void rotateStarted();
    void rotatedEnded();
    void twoFDrag(Vector2 p);
}
