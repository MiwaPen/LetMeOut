using TabsMenu;
using TabsMenu.Enums;

namespace Journal
{
    public class JournalTab : BaseMenuTab
    {
        public override BaseMenuTab Initialize(TabsMenuScreenMode mode)
        {
            return this;
        }

        public override void Show()
        {
            gameObject.SetActive(true);
        }

        public override void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}
