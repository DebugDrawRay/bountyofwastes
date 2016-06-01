using InControl;

public class PlayerActions : PlayerActionSet
{
    public PlayerAction LookUp;
    public PlayerAction LookDown;
    public PlayerAction LookLeft;
    public PlayerAction LookRight;

    public PlayerTwoAxisAction Look;

    public PlayerAction MoveForward;
    public PlayerAction MoveBack;
    public PlayerAction MoveLeft;
    public PlayerAction MoveRight;

    public PlayerTwoAxisAction Move;

    public PlayerAction Jump;
    public PlayerAction UseItem;
    public PlayerAction LockOn;

    public PlayerAction SwitchActionUp;
    public PlayerAction SwitchActionDown;

    public PlayerActions()
    {
        LookUp = CreatePlayerAction("Look Up");
        LookDown = CreatePlayerAction("Look Down");
        LookLeft = CreatePlayerAction("Look Left");
        LookRight = CreatePlayerAction("Look Right");

        Look = CreateTwoAxisPlayerAction(LookLeft, LookRight, LookDown, LookUp);

        MoveForward = CreatePlayerAction("Move Forward");
        MoveBack = CreatePlayerAction("Move Back");
        MoveLeft = CreatePlayerAction("Move Left");
        MoveRight = CreatePlayerAction("Move Right");

        Move = CreateTwoAxisPlayerAction(MoveLeft, MoveRight, MoveBack, MoveForward);

        Jump = CreatePlayerAction("Jump");
        UseItem = CreatePlayerAction("Use Item");
        LockOn = CreatePlayerAction("Lock On");

        SwitchActionUp = CreatePlayerAction("Switch Action Up");
        SwitchActionDown = CreatePlayerAction("Switch Action Down");
    }

    public static PlayerActions BindKeyboardAndJoystick()
    {
        PlayerActions actions = new PlayerActions();

        actions.LookUp.AddDefaultBinding(Mouse.PositiveY);
        actions.LookDown.AddDefaultBinding(Mouse.NegativeY);
        actions.LookLeft.AddDefaultBinding(Mouse.NegativeX);
        actions.LookRight.AddDefaultBinding(Mouse.PositiveX);

        actions.MoveForward.AddDefaultBinding(Key.W);
        actions.MoveBack.AddDefaultBinding(Key.S);
        actions.MoveLeft.AddDefaultBinding(Key.A);
        actions.MoveRight.AddDefaultBinding(Key.D);

        actions.Jump.AddDefaultBinding(Key.Space);
        actions.UseItem.AddDefaultBinding(Mouse.LeftButton);
        actions.LockOn.AddDefaultBinding(Mouse.RightButton);

        actions.SwitchActionUp.AddDefaultBinding(Mouse.PositiveScrollWheel);
        actions.SwitchActionDown.AddDefaultBinding(Mouse.NegativeScrollWheel);

        return actions;
    }
}
