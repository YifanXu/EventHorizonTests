using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface IAbilityScript
{
    PlayerAnimation.PlayerState GetState();

    void ExceuteAbility();
}