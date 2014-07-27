namespace BalloonsPopsGame.UserInterface
{
    using System;
    using Balloons;
    using Score;

    public abstract class UIHandler
    {
        private IBalloonsContainer container;

        public UIHandler(IBalloonsContainer container)
        {
            this.Container = container;
            this.Container.ContainerChanged += new EventHandler(this.ContainerChanged);
        }

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

        public abstract void DisplayMessage(string message, object placeholder);

        public abstract void DisplayMessage(string message, object firstPlaceholder, object secondPlaceholder);

        public abstract string ReadInput();

        public abstract void DisplayScoreboard(IScoreboard scoreboard);

        public abstract void DisplayBalloons();

        protected abstract void ContainerChanged(object sender, EventArgs e);      
    }
}
