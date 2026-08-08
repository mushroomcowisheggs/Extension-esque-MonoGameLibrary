using System;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Extensions.UserInterface;

namespace MonoGameLibrary.Adapters.Gum {
    public static class GumServiceExtensions {
        public static void AddTabForwardKey(this IUserInterfaceService serviceUserInterface, Keys key) {
            if (serviceUserInterface is GumService serviceGum) {
                serviceGum.AddTabForwardKey(key);
            } else {
                throw new InvalidOperationException(
                    "This method requires a GumService implementation. " +
                    "Make sure you called builder.UseGum() during host configuration."
                );
            }
        }
        
        public static void AddTabReverseKey(this IUserInterfaceService serviceUserInterface, Keys key) {
            if (serviceUserInterface is GumService serviceGum) {
                serviceGum.AddTabReverseKey(key);
            } else {
                throw new InvalidOperationException(
                    "This method requires a GumService implementation."
                );
            }
        }
        
        public static void RemoveTabForwardKey(this IUserInterfaceService serviceUserInterface, Keys key) {
            if (serviceUserInterface is GumService serviceGum) {
                serviceGum.RemoveTabForwardKey(key);
            } else {
                throw new InvalidOperationException(
                    "This method requires a GumService implementation."
                );
            }
        }
        
        public static void RemoveTabReverseKey(this IUserInterfaceService serviceUserInterface, Keys key) {
            if (serviceUserInterface is GumService serviceGum) {
                serviceGum.RemoveTabReverseKey(key);
            } else {
                throw new InvalidOperationException(
                    "This method requires a GumService implementation."
                );
            }
        }
    }
}