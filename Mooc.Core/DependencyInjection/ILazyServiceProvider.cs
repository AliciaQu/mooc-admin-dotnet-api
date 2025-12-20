namespace Mooc.Core.DependencyInjection;

/// <summary>
/// Lazy Instantiation，An object or resource is created only when it is first needed, 
/// rather than at the time of declaration or program startup
/// </summary>
public interface ILazyServiceProvider
{
    /// <summary>
    /// This method is equivalent of the GetRequiredService method.
    /// It does exists for backward compatibility.
    /// </summary>
    T LazyGetRequiredService<T>();

    /// <summary>
    /// This method is equivalent of the GetRequiredService method.
    /// It does exists for backward compatibility.
    /// </summary>
    object LazyGetRequiredService(Type serviceType);

    /// <summary>
    /// This method is equivalent of the GetService method.
    /// It does exists for backward compatibility.
    /// </summary>
    T? LazyGetService<T>();

    /// <summary>
    /// This method is equivalent of the GetService method.
    /// It does exists for backward compatibility.
    /// </summary>
    object? LazyGetService(Type serviceType);

    /// <summary>
    /// This method is equivalent of the <see cref="GetService{T}(T)"/> method.
    /// It does exists for backward compatibility.
    /// </summary>
    T LazyGetService<T>(T defaultValue);

    /// <summary>
    /// This method is equivalent of the <see cref="GetService(Type, object)"/> method.
    /// It does exists for backward compatibility.
    /// </summary>
    object LazyGetService(Type serviceType, object defaultValue);


    /// <summary>
    /// This method is equivalent of the <see cref="GetService{T}(Func{IServiceProvider, object})"/> method.
    /// It does exists for backward compatibility.
    /// </summary>
    T LazyGetService<T>(Func<IServiceProvider, object> factory);

}
