using System;
using System.ComponentModel;
using Avalonia.Input;

namespace Nodify.Compatibility;

public class MouseGesture : InputGesture
{
    //------------------------------------------------------
    //
    //  Constructors
    //
    //------------------------------------------------------

    #region Constructors
    /// <summary>
    ///  Constructor
    /// </summary>
    public MouseGesture()   // Mouse action
    {
    }

    /// <summary>
    ///  constructor
    /// </summary>
    /// <param name="mouseAction">Mouse Action</param>
    public MouseGesture(MouseAction mouseAction): this(mouseAction, KeyModifiers.None)
    {
    }

    /// <summary>
    ///  Constructor
    /// </summary>
    /// <param name="mouseAction">Mouse Action</param>
    /// <param name="modifiers">Modifiers</param>
    public MouseGesture( MouseAction mouseAction,KeyModifiers modifiers)   // acclerator action
    {
        if (!MouseGesture.IsDefinedMouseAction(mouseAction))
            throw new InvalidEnumArgumentException("mouseAction", (int)mouseAction, typeof(MouseAction));

        _modifiers = modifiers;
        _mouseAction = mouseAction;

        //AttachClassListeners();
    }
    #endregion Constructors
        
    //------------------------------------------------------
    //
    //  Public Methods
    //
    //------------------------------------------------------

    #region Public Methods
    /// <summary>
    /// Action 
    /// </summary>
    public MouseAction MouseAction
    {
        get 
        { 
            return _mouseAction; 
        }
        set 
        {
            if (!MouseGesture.IsDefinedMouseAction((MouseAction)value))
                throw new InvalidEnumArgumentException("value", (int)value, typeof(MouseAction));
            if (_mouseAction != value)
            {
                _mouseAction = (MouseAction)value;
                OnPropertyChanged("MouseAction");
            }
        }
    }

    /// <summary>
    /// Modifiers 
    /// </summary>
    public KeyModifiers Modifiers
    {
        get 
        { 
            return _modifiers; 
        }
        set 
        {
            if (_modifiers != value)
            {
                _modifiers = (KeyModifiers)value;
                OnPropertyChanged("Modifiers");
            }
        }
    }

    /// <summary>
    ///     Compares InputEventArgs with current Input
    /// </summary>
    /// <param name="targetElement">the element to receive the command</param>
    /// <param name="inputEventArgs">inputEventArgs to compare to</param>
    /// <returns>True - if matches, false otherwise.
    /// </returns>
    public override bool Matches(object targetElement, EventArgs inputEventArgs)
    {
        MouseAction mouseAction = GetMouseAction(inputEventArgs);
        if(mouseAction != MouseAction.None && inputEventArgs is PointerEventArgs e)
        {
            return ( ( (int)this.MouseAction == (int)mouseAction ) && ( this.Modifiers == e.KeyModifiers ) );
        }
        if(mouseAction != MouseAction.None && inputEventArgs is MouseButtonEventArgs mouseE)
        {
            return ( ( (int)this.MouseAction == (int)mouseAction ) && ( this.Modifiers == mouseE.KeyModifiers ) );
        }
        return false;
    }


    // Helper like Enum.IsDefined,  for MouseAction.
    internal static bool IsDefinedMouseAction(MouseAction mouseAction)
    {
        return (mouseAction >= MouseAction.None && mouseAction <= MouseAction.MiddleDoubleClick);
    }
    #endregion Public Methods

    #region Internal NotifyProperty changed

    /// <summary>
    ///     PropertyChanged event Handler
    /// </summary>
    internal event PropertyChangedEventHandler PropertyChanged;

    /// <summary>
    ///     PropertyChanged virtual
    /// </summary>
    internal virtual void OnPropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    #endregion

    //------------------------------------------------------
    //
    //  Internal Methods
    //
    //------------------------------------------------------
    #region Internal Methods

    internal static MouseAction GetMouseAction(EventArgs inputArgs)
    {
        MouseAction MouseAction = MouseAction.None;

        MouseButtonEventArgs args = inputArgs as MouseButtonEventArgs;
        if(args != null)
        {
            // if(inputArgs is MouseWheelEventArgs)
            // {
            //     MouseAction = MouseAction.WheelClick;
            // }
            // else
            {
                switch(args.ChangedButton)
                {
                    case MouseButton.Left:
                    {
                        if(args.ClickCount == 2)
                            MouseAction = MouseAction.LeftDoubleClick;
                        else if(args.ClickCount == 1)
                            MouseAction = MouseAction.LeftClick;
                    }
                        break;

                    case MouseButton.Right:
                    {
                        if(args.ClickCount == 2)
                            MouseAction = MouseAction.RightDoubleClick;
                        else if(args.ClickCount == 1)
                            MouseAction = MouseAction.RightClick;
                    }
                        break;

                    case MouseButton.Middle:
                    {
                        if(args.ClickCount == 2)
                            MouseAction = MouseAction.MiddleDoubleClick;
                        else if(args.ClickCount == 1)
                            MouseAction = MouseAction.MiddleClick;
                    }
                        break;
                }
            }
        }
            
        if(inputArgs is PointerWheelEventArgs)
        {
            MouseAction = MouseAction.WheelClick;
        }
        else if (inputArgs is PointerPressedEventArgs pointerPressed)
        {
            switch(pointerPressed.GetCurrentPoint(null).Properties.PointerUpdateKind)
            {
                case PointerUpdateKind.LeftButtonPressed:
                {
                    if(pointerPressed.ClickCount == 2)
                        MouseAction = MouseAction.LeftDoubleClick;
                    else if(pointerPressed.ClickCount == 1)
                        MouseAction = MouseAction.LeftClick;
                }
                    break;

                case PointerUpdateKind.RightButtonPressed:
                {
                    if(pointerPressed.ClickCount == 2)
                        MouseAction = MouseAction.RightDoubleClick;
                    else if(pointerPressed.ClickCount == 1)
                        MouseAction = MouseAction.RightClick;
                }
                    break;

                case PointerUpdateKind.MiddleButtonPressed:
                {
                    if(pointerPressed.ClickCount == 2)
                        MouseAction = MouseAction.MiddleDoubleClick;
                    else if(pointerPressed.ClickCount == 1)
                        MouseAction = MouseAction.MiddleClick;
                }
                    break;
            }
        }
        else if (inputArgs is PointerReleasedEventArgs pointerReleased)
        {
            switch(pointerReleased.GetCurrentPoint(null).Properties.PointerUpdateKind)
            {
                case PointerUpdateKind.LeftButtonReleased:
                {
                    MouseAction = MouseAction.LeftClick;
                }
                    break;

                case PointerUpdateKind.RightButtonReleased:
                {
                    MouseAction = MouseAction.RightClick;
                }
                    break;

                case PointerUpdateKind.MiddleButtonReleased:
                {
                    MouseAction = MouseAction.MiddleClick;
                }
                    break;
            }   
        }
        return MouseAction;
    }

    #endregion Internal Methods

    //------------------------------------------------------
    //
    //  Private Fields
    //
    //------------------------------------------------------
    #region Private Fields
    private MouseAction  _mouseAction = MouseAction.None;
    private KeyModifiers  _modifiers = KeyModifiers.None;
    //       private static bool _classRegistered = false;
    #endregion Private Fields
}