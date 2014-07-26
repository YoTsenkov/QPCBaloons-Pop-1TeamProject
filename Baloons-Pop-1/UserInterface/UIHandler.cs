namespace BalloonsPopsGame.UserInterface
{
    using System;
    using Balloons;

    public abstract class UIHandler
    {      
        private IBalloonsContainer container;

        public UIHandler(IBalloonsContainer container)
        {
            this.Container = container;
            this.Container.ContainerChanged += new EventHandler(ContainerChanged);
        }

        public abstract void DisplayMessage(string message);

        public abstract void DisplayMessage(string message, object placeholder);

        public abstract void DisplayMessage(string message, object firstPlaceholder, object secondPlaceholder);

        public abstract string ReadInput();

        public abstract void DisplayScoreboard(IScoreBoard scoreboard);

        protected abstract void ContainerChanged(object sender, EventArgs e);

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

        protected abstract void DisplayBalloons();
    }
}
