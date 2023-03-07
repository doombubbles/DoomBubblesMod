using System.Reflection;

namespace DoomBubblesMod.Utils;

public class DeclaringPreJITFilter : PreJITFilter
{
    public override bool ShouldJIT(MemberInfo member) =>
        member == null || base.ShouldJIT(member) && ShouldJIT(member.DeclaringType);
}