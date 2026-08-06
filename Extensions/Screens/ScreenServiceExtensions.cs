using System;

namespace MonoGameLibrary.Extensions.Screens {
    public static class ScreenServiceExtensions {
        public static bool IsEmpty(this IScreenService service) {
            return service == null || service.CurrentScreen == null;
        }
        
        public static Type GetCurrentScreenType(this IScreenService service) {
            if (service == null) {
                return null;
            }
            
            IScreen screenCurrent = service.CurrentScreen;
            if (screenCurrent == null) {
                return null;
            }
            
            return screenCurrent.GetType();
        }

        public static bool IsInScreen<T>(this IScreenService service) where T : Screen {
            return service != null && service.CurrentScreen is T;
        }
    }
}