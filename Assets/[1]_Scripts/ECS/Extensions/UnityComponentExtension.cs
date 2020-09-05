using UnityEngine;

namespace SA.Tanks.Extensions.UnityComponents
{
    public static class UnityComponentExtension
    {
        //получает компонент Provider с объекта через его компонент
        public static IEcsUnityProvider GetProvider(this Component component)
        {
            var gameObject = component.gameObject;
            
            IEcsUnityProvider provider;
            var providerExist = gameObject.TryGetComponent(out provider);
            if (!providerExist)
            {
                provider = gameObject.AddComponent<EcsUnityProvider>();
            }

            return provider;
        }
        

        //проверяет, есть ли компонент Provider на объекте
        public static bool HasProvider(this Component component)
        {
            var gameObject = component.gameObject;
            var providerExist = gameObject.TryGetComponent<IEcsUnityProvider>(out _);
            return providerExist;
        }
     }
}