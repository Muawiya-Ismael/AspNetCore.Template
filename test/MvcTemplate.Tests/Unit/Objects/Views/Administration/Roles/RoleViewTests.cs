using MvcTemplate.Components.Tree;
using Xunit;

namespace MvcTemplate.Objects
{
    public class RoleViewTests
    {
        [Fact]
        public void RoleView_CreatesEmpty()
        {
            MvcTree actual = new RoleView().Permissions;

            Assert.Empty(actual.SelectedIds);
            Assert.Empty(actual.Nodes);
        }
    }
}
