namespace BalloonsPopsGame
{
    using System;
    using Balloons;

    public abstract class UIGenerator
    {
        private BalloonsContainer container;

        public UIGenerator(BalloonsContainer container)
        {
            this.Container = container;
            this.Container.ContainerChanged += new EventHandler(ContainerContainerChanged);
        }

        protected abstract void ContainerContainerChanged(object sender, EventArgs e);

        protected BalloonsContainer Container
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
