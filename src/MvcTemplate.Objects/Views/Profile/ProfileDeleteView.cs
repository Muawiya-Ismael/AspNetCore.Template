using MvcTemplate.Components.Mvc;

namespace MvcTemplate.Objects;

public class ProfileDeleteView : AView
{
    [NotTrimmed]
    [StringLength(32)]
    public String Password { get; set; }
}
