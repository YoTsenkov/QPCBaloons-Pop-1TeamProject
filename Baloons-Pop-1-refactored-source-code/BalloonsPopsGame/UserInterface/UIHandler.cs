namespace BalloonsPopsGame.UserInterface
{
    using System;
    using Balloons;
    using Score;

    /// <summary>
    /// An abstract class for working with the user interface.
    /// </summary>
    public abstract class UIHandler
    {
        private IBalloonsContainer container;

        /// <summary>
        /// Initializes a new instance of the <see cref="UIHandler"/> class.
        /// </summary>
        /// <param name="container">The container to which event the class needs to subscribe to.</param>
        public UIHandler(IBalloonsContainer container)
        {
            this.Container = container;
            this.Container.ContainerChanged += new EventHandler(this.ContainerChanged);
        }

        /// <summary>
        /// Gets or sets the reference to the container.
        /// </summary>
        /// <value>Gets or sets the value to the container field.</value>
        protected IBalloonsContainer Container
        {
            get
            {
                return this.container;
            }

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("BalloonsContainer", "BalloonsContainer cannot be null!");
                }

                this.container = value;
            }
        }

        public abstract void DisplayMessage(string message);

        public abstract void DisplayMessage(string message, object argument);

        public abstract void DisplayMessage(string message, object firstArgument, object secondArgument);

        public abstract string ReadInput();

        public abstract void DisplayScoreboard(IScoreboard scoreboard);

        public abstract void DisplayBalloons();

        protected abstract void ContainerChanged(object sender, EventArgs e);      
    }
}
