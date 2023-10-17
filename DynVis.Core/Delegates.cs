namespace DynVis.Core
{
    //Делегаты не возращающие значения

    public delegate void Proc();
    public delegate void Proc<T>(T t);
    public delegate void Proc<T1, T2>(T1 t1, T2 t2);
    public delegate void Proc<T1, T2, T3>(T1 t1, T2 t2, T3 t3);
    public delegate void Proc<T1, T2, T3, T4>(T1 t1, T2 t2, T3 t3, T4 t4);
}
