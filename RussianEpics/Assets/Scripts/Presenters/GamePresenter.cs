using Services;
using VContainer.Unity;

namespace Presenters
{
    public class GamePresenter : ITickable
    {
        readonly HelloWorldService helloWorldService;

        public GamePresenter(HelloWorldService helloWorldService)
        {
            this.helloWorldService = helloWorldService;
        }

        void ITickable.Tick()
        {
            helloWorldService.Hello();
        }
    }
}
