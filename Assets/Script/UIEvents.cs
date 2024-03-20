using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public enum Type
{
    Length,
    Mass,
    Temperature,
    Volume,
    Speed,
    Area,
    ShoeSize,
    Gas,
    Custom,
}
public enum Unit_Langth
{
    mm,
    cm,
    m,
    km,
    inch,
    ft,
    yd,
    mile,
}
public enum Unit_Weight
{
    mg,
    g,
    kg,
    t,
    kt,
    grain,
    oz,
    lb,
}
public enum Unit_Temperature
{
    C,
    F,
    K,
}
public enum Unit_Volume
{
    cc,
    ml,
    dl,
    L,
    cm3,
    m3,
    in3,
    ft3,
    yd3,
    gal,
    bbl,
    oz,
}
public enum Unit_Speed
{
    m_s,
    m_h,
    km_s,
    km_h,
    in_s,
    in_h,
    ft_s,
    ft_h,
    mps,
    mph,
    kn,
    mach,
}
public enum Unit_Area
{
    m2,
    a,
    ha,
    km2,
    ft2,
    yd2,
    ac,
}
public enum Unit_Shoes
{
    Asia,
    US_M,
    US_W,
    US_B,
}
public enum Unit_Gas
{
    mpg,
    km_L,
}
public enum Unit_Custom
{
    MUL,
    DIV,
}


public class UIEvents : MonoBehaviour {

    public Dropdown dpType;
    public Type selectedType = Type.Length;

    public Dropdown dpUnitA;
    public int selectedIndex_A = 0;

    public Dropdown dpUnitB;
    public int selectedIndex_B = 1;

    public Dropdown dpCustom;
    public Unit_Custom selectedIndex_Custom = Unit_Custom.MUL;

    public InputField txtUnitA;
    public InputField txtUnitB;
    public InputField txtRatio;
    public Text labelRatio;

    double value_UnitA = 0.0;
    double value_UnitB = 0.0;
    double value_Ratio = 0.0;

    #region Default Function
    void Start()
    {
        Screen.SetResolution(720, 1280, true);
        List<string> names = new List<string>();

        dpType.ClearOptions();
        names.Clear();
        for (int i = 0; i < Enum.GetNames(typeof(Type)).Length; i++)
        {
            names.Add(((Type)i).ToString());
        }
        dpType.AddOptions(names);
        dpType.value = (int)Type.Custom;
        selectedType = Type.Custom;

        dpCustom.ClearOptions();
        names.Clear();
        for (int i = 0; i < Enum.GetNames(typeof(Unit_Custom)).Length; i++)
        {
            names.Add(((Unit_Custom)i).ToString());
        }
        dpCustom.AddOptions(names);
        dpCustom.value = (int)Unit_Custom.MUL;
        selectedIndex_Custom = Unit_Custom.MUL;

        dpUnitA.ClearOptions();
        dpUnitB.ClearOptions();
        txtRatio.gameObject.active = true;
        labelRatio.gameObject.active = true;
        dpCustom.gameObject.active = true;
    }

    void Update()
    {

    }
    #endregion


    #region User Function
    void ChangeType(Type t)
    {
        List<string> names = new List<string>();
        names.Clear();

        dpUnitA.ClearOptions();
        dpUnitB.ClearOptions();

        switch (t)
        {
            case Type.Length:
                for (int i = 0; i < Enum.GetNames(typeof(Unit_Langth)).Length; i++)
                {
                    names.Add(GetString(((Unit_Langth)i).ToString()));
                }
                break;
            case Type.Mass:
                for (int i = 0; i < Enum.GetNames(typeof(Unit_Weight)).Length; i++)
                {
                    names.Add(GetString(((Unit_Weight)i).ToString()));
                }
                break;
            case Type.Temperature:
                for (int i = 0; i < Enum.GetNames(typeof(Unit_Temperature)).Length; i++)
                {
                    names.Add(GetString(((Unit_Temperature)i).ToString()));
                }
                break;
            case Type.Volume:
                for (int i = 0; i < Enum.GetNames(typeof(Unit_Volume)).Length; i++)
                {
                    names.Add(GetString(((Unit_Volume)i).ToString()));
                }
                break;
            case Type.Speed:
                for (int i = 0; i < Enum.GetNames(typeof(Unit_Speed)).Length; i++)
                {
                    names.Add(GetString(((Unit_Speed)i).ToString()));
                }
                break;
            case Type.Area:
                for (int i = 0; i < Enum.GetNames(typeof(Unit_Area)).Length; i++)
                {
                    names.Add(GetString(((Unit_Area)i).ToString()));
                }
                break;
            case Type.ShoeSize:
                for (int i = 0; i < Enum.GetNames(typeof(Unit_Shoes)).Length; i++)
                {
                    names.Add(GetString(((Unit_Shoes)i).ToString()));
                }
                break;
            case Type.Gas:
                for (int i = 0; i < Enum.GetNames(typeof(Unit_Gas)).Length; i++)
                {
                    names.Add(GetString(((Unit_Gas)i).ToString()));
                }
                break;
            default:
                break;
        }

        dpUnitA.AddOptions(names);
        dpUnitB.AddOptions(names);
        if (dpType.value == (int)Type.Custom)
        {
            txtRatio.gameObject.active = true;
            labelRatio.gameObject.active = true;
            dpCustom.gameObject.active = true;
            dpUnitA.value = -1;
            dpUnitB.value = -1;
        }
        else
        {
            txtRatio.gameObject.active = false;
            labelRatio.gameObject.active = false;
            dpCustom.gameObject.active = false;
            dpUnitA.value = 0;
            dpUnitB.value = 0;
        }
        selectedType = t;
        selectedIndex_A = dpUnitA.value;
        selectedIndex_B = dpUnitB.value;
        txtUnitA.text = string.Empty;
        txtUnitB.text = string.Empty;
        txtRatio.text = string.Empty;
    }
    string GetString(string org)
    {
        string result = org;
        if (org.Contains("_"))
        {
            result = org.Replace('_', '/');
        }
        return result;
    }
    double ConvertUnit(Type t, int sourceType, int targetType, double sourceValue)
    {
        double result = 0.0;
        switch (t)
        {
            case Type.Length:
                result = ConvertLength((Unit_Langth)sourceType, (Unit_Langth)targetType, sourceValue);
                break;
            case Type.Mass:
                result = ConvertWeight((Unit_Weight)sourceType, (Unit_Weight)targetType, sourceValue);
                break;
            case Type.Temperature:
                result = ConvertTemperature((Unit_Temperature)sourceType, (Unit_Temperature)targetType, sourceValue);
                break;
            case Type.Volume:
                result = ConvertVolume((Unit_Volume)sourceType, (Unit_Volume)targetType, sourceValue);
                break;
            case Type.Speed:
                result = ConvertSpeed((Unit_Speed)sourceType, (Unit_Speed)targetType, sourceValue);
                break;
            case Type.Area:
                result = ConvertArea((Unit_Area)sourceType, (Unit_Area)targetType, sourceValue);
                break;
            case Type.ShoeSize:
                result = ConvertShoes((Unit_Shoes)sourceType, (Unit_Shoes)targetType, sourceValue);
                break;
            case Type.Gas:
                result = ConvertGas((Unit_Gas)sourceType, (Unit_Gas)targetType, sourceValue);
                break;
            case Type.Custom:
                result = ConvertCustom(sourceValue);
                break;
            default:
                break;
        }
        return result;
    }
    double ConvertLength(Unit_Langth sourceType, Unit_Langth targetType, double value)
    {
        double result = 0.0;
        switch (sourceType)
        {
            case Unit_Langth.mm:
                switch (targetType)
                {
                    case Unit_Langth.mm: result = value * 1.0; break;
                    case Unit_Langth.cm: result = value * 0.1; break;
                    case Unit_Langth.m: result = value * 0.001; break;
                    case Unit_Langth.km: result = value * 1e-6; break;
                    case Unit_Langth.inch: result = value * 0.03937; break;
                    case Unit_Langth.ft: result = value * 0.003281; break;
                    case Unit_Langth.yd: result = value * 0.001094; break;
                    case Unit_Langth.mile: result = value * 6.2137e-7; break;
                    default: break;
                }
                break;
            case Unit_Langth.cm:
                switch (targetType)
                {
                    case Unit_Langth.mm: result = value * 10.0; break;
                    case Unit_Langth.cm: result = value * 1.0; break;
                    case Unit_Langth.m: result = value * 0.01; break;
                    case Unit_Langth.km: result = value * 0.00001; break;
                    case Unit_Langth.inch: result = value * 0.393701; break;
                    case Unit_Langth.ft: result = value * 0.032808; break;
                    case Unit_Langth.yd: result = value * 0.010936; break;
                    case Unit_Langth.mile: result = value * 6.21371192237334e-6; break;
                    default: break;
                }
                break;
            case Unit_Langth.m:
                switch (targetType)
                {
                    case Unit_Langth.mm: result = value * 1000.0; break;
                    case Unit_Langth.cm: result = value * 100.0; break;
                    case Unit_Langth.m: result = value * 1.0; break;
                    case Unit_Langth.km: result = value * 0.001; break;
                    case Unit_Langth.inch: result = value * 39.370079; break;
                    case Unit_Langth.ft: result = value * 3.28084; break;
                    case Unit_Langth.yd: result = value * 1.093613; break;
                    case Unit_Langth.mile: result = value * 0.000621; break;
                    default: break;
                }
                break;
            case Unit_Langth.km:
                switch (targetType)
                {
                    case Unit_Langth.mm: result = value * 1000000.0; break;
                    case Unit_Langth.cm: result = value * 100000.0; break;
                    case Unit_Langth.m: result = value * 1000.0; break;
                    case Unit_Langth.km: result = value * 1.0; break;
                    case Unit_Langth.inch: result = value * 39370.0787; break;
                    case Unit_Langth.ft: result = value * 3280.8399; break;
                    case Unit_Langth.yd: result = value * 1093.6133; break;
                    case Unit_Langth.mile: result = value * 0.621371; break;
                    default: break;
                }
                break;
            case Unit_Langth.inch:
                switch (targetType)
                {
                    case Unit_Langth.mm: result = value * 25.4; break;
                    case Unit_Langth.cm: result = value * 2.54; break;
                    case Unit_Langth.m: result = value * 0.254; break;
                    case Unit_Langth.km: result = value * 0.000025; break;
                    case Unit_Langth.inch: result = value * 1.0; break;
                    case Unit_Langth.ft: result = value * 0.83333; break;
                    case Unit_Langth.yd: result = value * 0.27778; break;
                    case Unit_Langth.mile: result = value * 0.000016; break;
                    default: break;
                }
                break;
            case Unit_Langth.ft:
                switch (targetType)
                {
                    case Unit_Langth.mm: result = value * 304.8; break;
                    case Unit_Langth.cm: result = value * 30.48; break;
                    case Unit_Langth.m: result = value * 0.3048; break;
                    case Unit_Langth.km: result = value * 0.000305; break;
                    case Unit_Langth.inch: result = value * 12.0; break;
                    case Unit_Langth.ft: result = value * 1.0; break;
                    case Unit_Langth.yd: result = value * 0.333333; break;
                    case Unit_Langth.mile: result = value * 0.000189; break;
                    default: break;
                }
                break;
            case Unit_Langth.yd:
                switch (targetType)
                {
                    case Unit_Langth.mm: result = value * 914.4; break;
                    case Unit_Langth.cm: result = value * 91.44; break;
                    case Unit_Langth.m: result = value * 0.9144; break;
                    case Unit_Langth.km: result = value * 0.000914; break;
                    case Unit_Langth.inch: result = value * 36.0; break;
                    case Unit_Langth.ft: result = value * 3.0; break;
                    case Unit_Langth.yd: result = value * 1.0; break;
                    case Unit_Langth.mile: result = value * 0.000568; break;
                    default: break;
                }
                break;
            case Unit_Langth.mile:
                switch (targetType)
                {
                    case Unit_Langth.mm: result = value * 1609344; break;
                    case Unit_Langth.cm: result = value * 160934.4; break;
                    case Unit_Langth.m: result = value * 1609.344; break;
                    case Unit_Langth.km: result = value * 1.609344; break;
                    case Unit_Langth.inch: result = value * 63360; break;
                    case Unit_Langth.ft: result = value * 5280; break;
                    case Unit_Langth.yd: result = value * 1760; break;
                    case Unit_Langth.mile: result = value * 1.0; break;
                    default: break;
                }
                break;
            default:
                break;
        }
        return result;
    }
    double ConvertWeight(Unit_Weight sourceType, Unit_Weight targetType, double value)
    {
        double result = 0.0;
        switch (sourceType)
        {
            case Unit_Weight.mg:
                switch (targetType)
                {
                    case Unit_Weight.mg: result = value * 1.0; break;
                    case Unit_Weight.g: result = value * 0.001; break;
                    case Unit_Weight.kg: result = value * 1e-6; break;
                    case Unit_Weight.t: result = value * 10e-10; break;
                    case Unit_Weight.kt: result = value * 1e12; break;
                    case Unit_Weight.grain: result = value * 0.015432; break;
                    case Unit_Weight.oz: result = value * 0.000035; break;
                    case Unit_Weight.lb: result = value * 2.2046e-6; break;
                    default: break;
                }
                break;
            case Unit_Weight.g:
                switch (targetType)
                {
                    case Unit_Weight.mg: result = value * 1000.0; break;
                    case Unit_Weight.g: result = value * 1.0; break;
                    case Unit_Weight.kg: result = value * 0.001; break;
                    case Unit_Weight.t: result = value * 1e-6; break;
                    case Unit_Weight.kt: result = value * 1e-9; break;
                    case Unit_Weight.grain: result = value * 15.432358; break;
                    case Unit_Weight.oz: result = value * 0.035274; break;
                    case Unit_Weight.lb: result = value * 0.002205; break;
                    default: break;
                }
                break;
            case Unit_Weight.kg:
                switch (targetType)
                {
                    case Unit_Weight.mg: result = value * 1000000.0; break;
                    case Unit_Weight.g: result = value * 1000.0; break;
                    case Unit_Weight.kg: result = value * 1.0; break;
                    case Unit_Weight.t: result = value * 0.001; break;
                    case Unit_Weight.kt: result = value * 1e-6; break;
                    case Unit_Weight.grain: result = value * 15432.3584; break;
                    case Unit_Weight.oz: result = value * 35.273962; break;
                    case Unit_Weight.lb: result = value * 2.204623; break;
                    default: break;
                }
                break;
            case Unit_Weight.t:
                switch (targetType)
                {
                    case Unit_Weight.mg: result = value * 1e+9; break;
                    case Unit_Weight.g: result = value * 1000000.0; break;
                    case Unit_Weight.kg: result = value * 1000.0; break;
                    case Unit_Weight.t: result = value * 1.0; break;
                    case Unit_Weight.kt: result = value * 0.001; break;
                    case Unit_Weight.grain: result = value * 15432358.4; break;
                    case Unit_Weight.oz: result = value * 35273.9619; break;
                    case Unit_Weight.lb: result = value * 2204.62262; break;
                    default: break;
                }
                break;
            case Unit_Weight.kt:
                switch (targetType)
                {
                    case Unit_Weight.mg: result = value * 10e+11; break;
                    case Unit_Weight.g: result = value * 1000000000; break;
                    case Unit_Weight.kg: result = value * 1000000; break;
                    case Unit_Weight.t: result = value * 1000; break;
                    case Unit_Weight.kt: result = value * 1.0; break;
                    case Unit_Weight.grain: result = value * 1.5432e+10; break;
                    case Unit_Weight.oz: result = value * 35273961.9; break;
                    case Unit_Weight.lb: result = value * 2204622.62; break;
                    default: break;
                }
                break;
            case Unit_Weight.grain:
                switch (targetType)
                {
                    case Unit_Weight.mg: result = value * 64.79891; break;
                    case Unit_Weight.g: result = value * 0.064799; break;
                    case Unit_Weight.kg: result = value * 0.000065; break;
                    case Unit_Weight.t: result = value * 6.4799e-8; break;
                    case Unit_Weight.kt: result = value * 6.4799e-11; break;
                    case Unit_Weight.grain: result = value * 1.0; break;
                    case Unit_Weight.oz: result = value * 0.002286; break;
                    case Unit_Weight.lb: result = value * 0.000143; break;
                    default: break;
                }
                break;
            case Unit_Weight.oz:
                switch (targetType)
                {
                    case Unit_Weight.mg: result = value * 28349.5231; break;
                    case Unit_Weight.g: result = value * 28.349523; break;
                    case Unit_Weight.kg: result = value * 0.02835; break;
                    case Unit_Weight.t: result = value * 0.000028; break;
                    case Unit_Weight.kt: result = value * 2.835e-8; break;
                    case Unit_Weight.grain: result = value * 437.5; break;
                    case Unit_Weight.oz: result = value * 1.0; break;
                    case Unit_Weight.lb: result = value * 0.0625; break;
                    default: break;
                }
                break;
            case Unit_Weight.lb:
                switch (targetType)
                {
                    case Unit_Weight.mg: result = value * 453592.37; break;
                    case Unit_Weight.g: result = value * 453.59237; break;
                    case Unit_Weight.kg: result = value * 0.453592; break;
                    case Unit_Weight.t: result = value * 0.000454; break;
                    case Unit_Weight.kt: result = value * 4.5359e-7; break;
                    case Unit_Weight.grain: result = value * 7000; break;
                    case Unit_Weight.oz: result = value * 16.0; break;
                    case Unit_Weight.lb: result = value * 1.0; break;
                    default: break;
                }
                break;
            default:
                break;
        }
        return result;
    }
    double ConvertTemperature(Unit_Temperature sourceType, Unit_Temperature targetType, double value)
    {
        double result = 0.0;
        switch (sourceType)
        {
            case Unit_Temperature.C:
                switch (targetType)
                {
                    case Unit_Temperature.C: result = value * 1.0; break;
                    case Unit_Temperature.F: result = (value * 1.8) + 32.0; break;
                    case Unit_Temperature.K: result = value + 273.0; break;
                    default: break;
                }
                break;
            case Unit_Temperature.F:
                switch (targetType)
                {
                    case Unit_Temperature.C: result = (value - 32.0) / 1.8; break;
                    case Unit_Temperature.F: result = value * 1.0; break;
                    case Unit_Temperature.K: result = ((value - 32.0) / 1.8) + 273.0; break;
                    default: break;
                }
                break;
            case Unit_Temperature.K:
                switch (targetType)
                {
                    case Unit_Temperature.C: result = value - 273.0; break;
                    case Unit_Temperature.F: result = (value - 273) * 1.8 + 32.0; break;
                    case Unit_Temperature.K: result = value * 1.0; break;
                    default: break;
                }
                break;
            default:
                break;
        }
        return result;
    }
    double ConvertVolume(Unit_Volume sourceType, Unit_Volume targetType, double value)
    {
        double result = 0.0;
        switch (sourceType)
        {
            case Unit_Volume.cc:
                switch (targetType)
                {
                    case Unit_Volume.cc: result = value * 1.0; break;
                    case Unit_Volume.ml: result = value * 1.0; break;
                    case Unit_Volume.dl: result = value * 0.01; break;
                    case Unit_Volume.L: result = value * 0.001; break;
                    case Unit_Volume.cm3: result = value * 1.0; break;
                    case Unit_Volume.m3: result = value * 1e-6; break;
                    case Unit_Volume.in3: result = value * 0.061024; break;
                    case Unit_Volume.ft3: result = value * 0.000035; break;
                    case Unit_Volume.yd3: result = value * 1.308e-6; break;
                    case Unit_Volume.gal: result = value * 0.000264; break;
                    case Unit_Volume.bbl: result = value * 6.2933e-6; break;
                    case Unit_Volume.oz: result = value * 0.033814; break;
                    default: break;
                }
                break;
            case Unit_Volume.ml:
                switch (targetType)
                {
                    case Unit_Volume.cc: result = value * 1.0; break;
                    case Unit_Volume.ml: result = value * 1.0; break;
                    case Unit_Volume.dl: result = value * 0.01; break;
                    case Unit_Volume.L: result = value * 0.001; break;
                    case Unit_Volume.cm3: result = value * 1.0; break;
                    case Unit_Volume.m3: result = value * 1e-6; break;
                    case Unit_Volume.in3: result = value * 0.061024; break;
                    case Unit_Volume.ft3: result = value * 0.000035; break;
                    case Unit_Volume.yd3: result = value * 1.308e-6; break;
                    case Unit_Volume.gal: result = value * 0.000264; break;
                    case Unit_Volume.bbl: result = value * 6.2933e-6; break;
                    case Unit_Volume.oz: result = value * 0.033814; break;
                    default: break;
                }
                break;
            case Unit_Volume.dl:
                switch (targetType)
                {
                    case Unit_Volume.cc: result = value * 100.0; break;
                    case Unit_Volume.ml: result = value * 100.0; break;
                    case Unit_Volume.dl: result = value * 1.0; break;
                    case Unit_Volume.L: result = value * 0.1; break;
                    case Unit_Volume.cm3: result = value * 100.0; break;
                    case Unit_Volume.m3: result = value * 0.0001; break;
                    case Unit_Volume.in3: result = value * 6.102374; break;
                    case Unit_Volume.ft3: result = value * 0.003531; break;
                    case Unit_Volume.yd3: result = value * 0.000131; break;
                    case Unit_Volume.gal: result = value * 0.026417; break;
                    case Unit_Volume.bbl: result = value * 0.000629; break;
                    case Unit_Volume.oz: result = value * 3.381402; break;
                    default: break;
                }
                break;
            case Unit_Volume.L:
                switch (targetType)
                {
                    case Unit_Volume.cc: result = value * 1000.0; break;
                    case Unit_Volume.ml: result = value * 1000.0; break;
                    case Unit_Volume.dl: result = value * 10.0; break;
                    case Unit_Volume.L: result = value * 1.0; break;
                    case Unit_Volume.cm3: result = value * 1000.0; break;
                    case Unit_Volume.m3: result = value * 0.001; break;
                    case Unit_Volume.in3: result = value * 61.023744; break;
                    case Unit_Volume.ft3: result = value * 0.035315; break;
                    case Unit_Volume.yd3: result = value * 0.001308; break;
                    case Unit_Volume.gal: result = value * 0.264172; break;
                    case Unit_Volume.bbl: result = value * 0.006293; break;
                    case Unit_Volume.oz: result = value * 33.814022; break;
                    default: break;
                }
                break;
            case Unit_Volume.cm3:
                switch (targetType)
                {
                    case Unit_Volume.cc: result = value * 1.0; break;
                    case Unit_Volume.ml: result = value * 1.0; break;
                    case Unit_Volume.dl: result = value * 0.01; break;
                    case Unit_Volume.L: result = value * 0.001; break;
                    case Unit_Volume.cm3: result = value * 1.0; break;
                    case Unit_Volume.m3: result = value * 1e-6; break;
                    case Unit_Volume.in3: result = value * 0.061024; break;
                    case Unit_Volume.ft3: result = value * 0.000035; break;
                    case Unit_Volume.yd3: result = value * 1.308e-6; break;
                    case Unit_Volume.gal: result = value * 0.000264; break;
                    case Unit_Volume.bbl: result = value * 6.2933e-6; break;
                    case Unit_Volume.oz: result = value * 0.033814; break;
                    default: break;
                }
                break;
            case Unit_Volume.m3:
                switch (targetType)
                {
                    case Unit_Volume.cc: result = value * 1000000.0; break;
                    case Unit_Volume.ml: result = value * 1000000.0; break;
                    case Unit_Volume.dl: result = value * 10000.0; break;
                    case Unit_Volume.L: result = value * 1000.0; break;
                    case Unit_Volume.cm3: result = value * 1000000.0; break;
                    case Unit_Volume.m3: result = value * 1.0; break;
                    case Unit_Volume.in3: result = value * 61023.7441; break;
                    case Unit_Volume.ft3: result = value * 35.314667; break;
                    case Unit_Volume.yd3: result = value * 1.307951; break;
                    case Unit_Volume.gal: result = value * 264.172052; break;
                    case Unit_Volume.bbl: result = value * 6.293266; break;
                    case Unit_Volume.oz: result = value * 33814.0222; break;
                    default: break;
                }
                break;
            case Unit_Volume.in3:
                switch (targetType)
                {
                    case Unit_Volume.cc: result = value * 16.387064; break;
                    case Unit_Volume.ml: result = value * 16.387064; break;
                    case Unit_Volume.dl: result = value * 0.163871; break;
                    case Unit_Volume.L: result = value * 0.016387; break;
                    case Unit_Volume.cm3: result = value * 16.387064; break;
                    case Unit_Volume.m3: result = value * 0.000016; break;
                    case Unit_Volume.in3: result = value * 1.0; break;
                    case Unit_Volume.ft3: result = value * 0.000579; break;
                    case Unit_Volume.yd3: result = value * 0.000021; break;
                    case Unit_Volume.gal: result = value * 0.004329; break;
                    case Unit_Volume.bbl: result = value * 0.000103; break;
                    case Unit_Volume.oz: result = value * 0.554113; break;
                    default: break;
                }
                break;
            case Unit_Volume.ft3:
                switch (targetType)
                {
                    case Unit_Volume.cc: result = value * 28316.8466; break;
                    case Unit_Volume.ml: result = value * 28316.8466; break;
                    case Unit_Volume.dl: result = value * 283.168466; break;
                    case Unit_Volume.L: result = value * 28.316847; break;
                    case Unit_Volume.cm3: result = value * 28316.8466; break;
                    case Unit_Volume.m3: result = value * 0.028317; break;
                    case Unit_Volume.in3: result = value * 1728.0; break;
                    case Unit_Volume.ft3: result = value * 1.0; break;
                    case Unit_Volume.yd3: result = value * 0.037037; break;
                    case Unit_Volume.gal: result = value * 7.480519; break;
                    case Unit_Volume.bbl: result = value * 0.178205; break;
                    case Unit_Volume.oz: result = value * 957.506479; break;
                    default: break;
                }
                break;
            case Unit_Volume.yd3:
                switch (targetType)
                {
                    case Unit_Volume.cc: result = value * 764554.858; break;
                    case Unit_Volume.ml: result = value * 764554.858; break;
                    case Unit_Volume.dl: result = value * 7645.54858; break;
                    case Unit_Volume.L: result = value * 764.554858; break;
                    case Unit_Volume.cm3: result = value * 764554.858; break;
                    case Unit_Volume.m3: result = value * 0.764555; break;
                    case Unit_Volume.in3: result = value * 46656.0; break;
                    case Unit_Volume.ft3: result = value * 27.0; break;
                    case Unit_Volume.yd3: result = value * 1.0; break;
                    case Unit_Volume.gal: result = value * 201.974026; break;
                    case Unit_Volume.bbl: result = value * 4.811547; break;
                    case Unit_Volume.oz: result = value * 25852.6749; break;
                    default: break;
                }
                break;
            case Unit_Volume.gal:
                switch (targetType)
                {
                    case Unit_Volume.cc: result = value * 3785.41178; break;
                    case Unit_Volume.ml: result = value * 3785.41178; break;
                    case Unit_Volume.dl: result = value * 37.854118; break;
                    case Unit_Volume.L: result = value * 3.785412; break;
                    case Unit_Volume.cm3: result = value * 3785.41178; break;
                    case Unit_Volume.m3: result = value * 0.003785; break;
                    case Unit_Volume.in3: result = value * 231.0; break;
                    case Unit_Volume.ft3: result = value * 0.133681; break;
                    case Unit_Volume.yd3: result = value * 0.004951; break;
                    case Unit_Volume.gal: result = value * 1.0; break;
                    case Unit_Volume.bbl: result = value * 0.023823; break;
                    case Unit_Volume.oz: result = value * 127.999998; break;
                    default: break;
                }
                break;
            case Unit_Volume.bbl:
                switch (targetType)
                {
                    case Unit_Volume.cc: result = value * 158900; break;
                    case Unit_Volume.ml: result = value * 158900; break;
                    case Unit_Volume.dl: result = value * 1589.0; break;
                    case Unit_Volume.L: result = value * 158.9; break;
                    case Unit_Volume.cm3: result = value * 158900; break;
                    case Unit_Volume.m3: result = value * 0.1589; break;
                    case Unit_Volume.in3: result = value * 9696.67294; break;
                    case Unit_Volume.ft3: result = value * 5.611501; break;
                    case Unit_Volume.yd3: result = value * 0.207833; break;
                    case Unit_Volume.gal: result = value * 41.976939; break;
                    case Unit_Volume.bbl: result = value * 1.0; break;
                    case Unit_Volume.oz: result = value * 5373.04813; break;
                    default: break;
                }
                break;
            case Unit_Volume.oz:
                switch (targetType)
                {
                    case Unit_Volume.cc: result = value * 29.57353; break;
                    case Unit_Volume.ml: result = value * 29.57353; break;
                    case Unit_Volume.dl: result = value * 0.295735; break;
                    case Unit_Volume.L: result = value * 0.029574; break;
                    case Unit_Volume.cm3: result = value * 29.57353; break;
                    case Unit_Volume.m3: result = value * 0.00003; break;
                    case Unit_Volume.in3: result = value * 1.804688; break;
                    case Unit_Volume.ft3: result = value * 0.001044; break;
                    case Unit_Volume.yd3: result = value * 0.00003868; break;
                    case Unit_Volume.gal: result = value * 0.007813; break;
                    case Unit_Volume.bbl: result = value * 0.000186; break;
                    case Unit_Volume.oz: result = value * 1.0; break;
                    default: break;
                }
                break;
            default:
                break;
        }
        return result;
    }
    double ConvertSpeed(Unit_Speed sourceType, Unit_Speed targetType, double value)
    {
        double result = 0.0;
        switch (sourceType)
        {
            case Unit_Speed.m_s:
                switch (targetType)
                {
                    case Unit_Speed.m_s: result = value * 1.0; break;
                    case Unit_Speed.m_h: result = value * 3600; break;
                    case Unit_Speed.km_s: result = value * 0.001; break;
                    case Unit_Speed.km_h: result = value * 3.6; break;
                    case Unit_Speed.in_s: result = value * 39.370079; break;
                    case Unit_Speed.in_h: result = value * 141732.283; break;
                    case Unit_Speed.ft_s: result = value * 3.28084; break;
                    case Unit_Speed.ft_h: result = value * 11811.0236; break;
                    case Unit_Speed.mps: result = value * 0.000621; break;
                    case Unit_Speed.mph: result = value * 2.236936; break;
                    case Unit_Speed.kn: result = value * 1.943844; break;
                    case Unit_Speed.mach: result = value * 0.002941; break;
                    default: break;
                }
                break;
            case Unit_Speed.m_h:
                switch (targetType)
                {
                    case Unit_Speed.m_s: result = value * 0.000278; break;
                    case Unit_Speed.m_h: result = value * 1.0; break;
                    case Unit_Speed.km_s: result = value * 2.7778e-7; break;
                    case Unit_Speed.km_h: result = value * 0.001; break;
                    case Unit_Speed.in_s: result = value * 0.010936; break;
                    case Unit_Speed.in_h: result = value * 39.370079; break;
                    case Unit_Speed.ft_s: result = value * 0.000911; break;
                    case Unit_Speed.ft_h: result = value * 3.28084; break;
                    case Unit_Speed.mps: result = value * 1.726e-7; break;
                    case Unit_Speed.mph: result = value * 0.000621; break;
                    case Unit_Speed.kn: result = value * 0.00054; break;
                    case Unit_Speed.mach: result = value * 8.1699e-7; break;
                    default: break;
                }
                break;
            case Unit_Speed.km_s:
                switch (targetType)
                {
                    case Unit_Speed.m_s: result = value * 1000.0; break;
                    case Unit_Speed.m_h: result = value * 3600000.0; break;
                    case Unit_Speed.km_s: result = value * 1.0; break;
                    case Unit_Speed.km_h: result = value * 3600.0; break;
                    case Unit_Speed.in_s: result = value * 39370.0787; break;
                    case Unit_Speed.in_h: result = value * 141732283.0; break;
                    case Unit_Speed.ft_s: result = value * 3280.8399; break;
                    case Unit_Speed.ft_h: result = value * 11811023.6; break;
                    case Unit_Speed.mps: result = value * 0.621371; break;
                    case Unit_Speed.mph: result = value * 2236.93629; break;
                    case Unit_Speed.kn: result = value * 1943.84449; break;
                    case Unit_Speed.mach: result = value * 2.941176; break;
                    default: break;
                }
                break;
            case Unit_Speed.km_h:
                switch (targetType)
                {
                    case Unit_Speed.m_s: result = value * 0.277778; break;
                    case Unit_Speed.m_h: result = value * 1000.0; break;
                    case Unit_Speed.km_s: result = value * 0.000278; break;
                    case Unit_Speed.km_h: result = value * 1.0; break;
                    case Unit_Speed.in_s: result = value * 10.936133; break;
                    case Unit_Speed.in_h: result = value * 39370.0787; break;
                    case Unit_Speed.ft_s: result = value * 0.911344; break;
                    case Unit_Speed.ft_h: result = value * 3280.8399; break;
                    case Unit_Speed.mps: result = value * 0.000173; break;
                    case Unit_Speed.mph: result = value * 0.621371; break;
                    case Unit_Speed.kn: result = value * 0.539957; break;
                    case Unit_Speed.mach: result = value * 0.000817; break;
                    default: break;
                }
                break;
            case Unit_Speed.in_s:
                switch (targetType)
                {
                    case Unit_Speed.m_s: result = value * 0.0254; break;
                    case Unit_Speed.m_h: result = value * 91.44; break;
                    case Unit_Speed.km_s: result = value * 0.000025; break;
                    case Unit_Speed.km_h: result = value * 0.09144; break;
                    case Unit_Speed.in_s: result = value * 1.0; break;
                    case Unit_Speed.in_h: result = value * 3600.0; break;
                    case Unit_Speed.ft_s: result = value * 0.083333; break;
                    case Unit_Speed.ft_h: result = value * 300.0; break;
                    case Unit_Speed.mps: result = value * 0.000016; break;
                    case Unit_Speed.mph: result = value * 0.056818; break;
                    case Unit_Speed.kn: result = value * 0.049374; break;
                    case Unit_Speed.mach: result = value * 0.000075; break;
                    default: break;
                }
                break;
            case Unit_Speed.in_h:
                switch (targetType)
                {
                    case Unit_Speed.m_s: result = value * 7.0556e-6; break;
                    case Unit_Speed.m_h: result = value * 0.0254; break;
                    case Unit_Speed.km_s: result = value * 7.0556e-9; break;
                    case Unit_Speed.km_h: result = value * 0.000025; break;
                    case Unit_Speed.in_s: result = value * 0.000278; break;
                    case Unit_Speed.in_h: result = value * 1.0; break;
                    case Unit_Speed.ft_s: result = value * 0.000023; break;
                    case Unit_Speed.ft_h: result = value * 0.083333; break;
                    case Unit_Speed.mps: result = value * 4.3841e-9; break;
                    case Unit_Speed.mph: result = value * 0.000016; break;
                    case Unit_Speed.kn: result = value * 0.000014; break;
                    case Unit_Speed.mach: result = value * 2.0752e-8; break;
                    default: break;
                }
                break;
            case Unit_Speed.ft_s:
                switch (targetType)
                {
                    case Unit_Speed.m_s: result = value * 0.3048; break;
                    case Unit_Speed.m_h: result = value * 1097.28; break;
                    case Unit_Speed.km_s: result = value * 0.000305; break;
                    case Unit_Speed.km_h: result = value * 1.09728; break;
                    case Unit_Speed.in_s: result = value * 12.0; break;
                    case Unit_Speed.in_h: result = value * 43200.0; break;
                    case Unit_Speed.ft_s: result = value * 1.0; break;
                    case Unit_Speed.ft_h: result = value * 3600.0; break;
                    case Unit_Speed.mps: result = value * 0.000189; break;
                    case Unit_Speed.mph: result = value * 0.681818; break;
                    case Unit_Speed.kn: result = value * 0.592484; break;
                    case Unit_Speed.mach: result = value * 0.000896; break;
                    default: break;
                }
                break;
            case Unit_Speed.ft_h:
                switch (targetType)
                {
                    case Unit_Speed.m_s: result = value * 0.000085; break;
                    case Unit_Speed.m_h: result = value * 0.3048; break;
                    case Unit_Speed.km_s: result = value * 8.4667e-8; break;
                    case Unit_Speed.km_h: result = value * 0.000305; break;
                    case Unit_Speed.in_s: result = value * 0.003333; break;
                    case Unit_Speed.in_h: result = value * 12.0; break;
                    case Unit_Speed.ft_s: result = value * 0.000278; break;
                    case Unit_Speed.ft_h: result = value * 1.0; break;
                    case Unit_Speed.mps: result = value * 5.2609e-8; break;
                    case Unit_Speed.mph: result = value * 0.000189; break;
                    case Unit_Speed.kn: result = value * 0.000165; break;
                    case Unit_Speed.mach: result = value * 2.4902e-7; break;
                    default: break;
                }
                break;
            case Unit_Speed.mps:
                switch (targetType)
                {
                    case Unit_Speed.m_s: result = value * 1609.344; break;
                    case Unit_Speed.m_h: result = value * 5793638.4; break;
                    case Unit_Speed.km_s: result = value * 1.609344; break;
                    case Unit_Speed.km_h: result = value * 5793.6384; break;
                    case Unit_Speed.in_s: result = value * 63360.0; break;
                    case Unit_Speed.in_h: result = value * 228096000.0; break;
                    case Unit_Speed.ft_s: result = value * 5280.0; break;
                    case Unit_Speed.ft_h: result = value * 19008000.0; break;
                    case Unit_Speed.mps: result = value * 1.0; break;
                    case Unit_Speed.mph: result = value * 3600.0; break;
                    case Unit_Speed.kn: result = value * 3128.31447; break;
                    case Unit_Speed.mach: result = value * 4.733365; break;
                    default: break;
                }
                break;
            case Unit_Speed.mph:
                switch (targetType)
                {
                    case Unit_Speed.m_s: result = value * 0.44704; break;
                    case Unit_Speed.m_h: result = value * 1609.344; break;
                    case Unit_Speed.km_s: result = value * 0.000447; break;
                    case Unit_Speed.km_h: result = value * 1.609344; break;
                    case Unit_Speed.in_s: result = value * 17.6; break;
                    case Unit_Speed.in_h: result = value * 63360; break;
                    case Unit_Speed.ft_s: result = value * 1.466667; break;
                    case Unit_Speed.ft_h: result = value * 5280.0; break;
                    case Unit_Speed.mps: result = value * 0.000278; break;
                    case Unit_Speed.mph: result = value * 1.0; break;
                    case Unit_Speed.kn: result = value * 0.868976; break;
                    case Unit_Speed.mach: result = value * 0.001315; break;
                    default: break;
                }
                break;
            case Unit_Speed.kn:
                switch (targetType)
                {
                    case Unit_Speed.m_s: result = value * 0.514444; break;
                    case Unit_Speed.m_h: result = value * 1852.0; break;
                    case Unit_Speed.km_s: result = value * 0.000514; break;
                    case Unit_Speed.km_h: result = value * 1.852; break;
                    case Unit_Speed.in_s: result = value * 20.253718; break;
                    case Unit_Speed.in_h: result = value * 72913.3858; break;
                    case Unit_Speed.ft_s: result = value * 1.68781; break;
                    case Unit_Speed.ft_h: result = value * 6076.11549; break;
                    case Unit_Speed.mps: result = value * 0.00032; break;
                    case Unit_Speed.mph: result = value * 1.150779; break;
                    case Unit_Speed.kn: result = value * 1.0; break;
                    case Unit_Speed.mach: result = value * 0.001513; break;
                    default: break;
                }
                break;
            case Unit_Speed.mach:
                switch (targetType)
                {
                    case Unit_Speed.m_s: result = value * 340.0; break;
                    case Unit_Speed.m_h: result = value * 1224000.0; break;
                    case Unit_Speed.km_s: result = value * 0.34; break;
                    case Unit_Speed.km_h: result = value * 1224.0; break;
                    case Unit_Speed.in_s: result = value * 13385.8268; break;
                    case Unit_Speed.in_h: result = value * 48188976.4; break;
                    case Unit_Speed.ft_s: result = value * 1115.48556; break;
                    case Unit_Speed.ft_h: result = value * 4015748.03; break;
                    case Unit_Speed.mps: result = value * 0.211266; break;
                    case Unit_Speed.mph: result = value * 760.558339; break;
                    case Unit_Speed.kn: result = value * 660.907127; break;
                    case Unit_Speed.mach: result = value * 1.0; break;
                    default: break;
                }
                break;
            default:
                break;
        }
        return result;
    }
    double ConvertArea(Unit_Area sourceType, Unit_Area targetType, double value)
    {
        double result = 0;
        switch (sourceType)
        {
            case Unit_Area.m2:
                switch (targetType)
                {
                    case Unit_Area.m2: result = value * 1.0; break;
                    case Unit_Area.a: result = value * 0.01; break;
                    case Unit_Area.ha: result = value * 0.0001; break;
                    case Unit_Area.km2: result = value * 1e-6; break;
                    case Unit_Area.ft2: result = value * 10.76391; break;
                    case Unit_Area.yd2: result = value * 1.19599; break;
                    case Unit_Area.ac: result = value * 0.000247; break;
                    default: break;
                }
                break;
            case Unit_Area.a:
                switch (targetType)
                {
                    case Unit_Area.m2: result = value * 100.0; break;
                    case Unit_Area.a: result = value * 1.0; break;
                    case Unit_Area.ha: result = value * 0.01; break;
                    case Unit_Area.km2: result = value * 0.0001; break;
                    case Unit_Area.ft2: result = value * 1076.39104; break;
                    case Unit_Area.yd2: result = value * 119.599005; break;
                    case Unit_Area.ac: result = value * 0.024711; break;
                    default: break;
                }
                break;
            case Unit_Area.ha:
                switch (targetType)
                {
                    case Unit_Area.m2: result = value * 10000.0; break;
                    case Unit_Area.a: result = value * 100.0; break;
                    case Unit_Area.ha: result = value * 1.0; break;
                    case Unit_Area.km2: result = value * 0.01; break;
                    case Unit_Area.ft2: result = value * 107639.104; break;
                    case Unit_Area.yd2: result = value * 11959.9005; break;
                    case Unit_Area.ac: result = value * 2.471054; break;
                    default: break;
                }
                break;
            case Unit_Area.km2:
                switch (targetType)
                {
                    case Unit_Area.m2: result = value * 1000000.0; break;
                    case Unit_Area.a: result = value * 10000.0; break;
                    case Unit_Area.ha: result = value * 100.0; break;
                    case Unit_Area.km2: result = value * 1.0; break;
                    case Unit_Area.ft2: result = value * 10763910.4; break;
                    case Unit_Area.yd2: result = value * 1195990.05; break;
                    case Unit_Area.ac: result = value * 247.105381; break;
                    default: break;
                }
                break;
            case Unit_Area.ft2:
                switch (targetType)
                {
                    case Unit_Area.m2: result = value * 0.092903; break;
                    case Unit_Area.a: result = value * 0.000929; break;
                    case Unit_Area.ha: result = value * 9.2903e-6; break;
                    case Unit_Area.km2: result = value * 9.2903e-8; break;
                    case Unit_Area.ft2: result = value * 1.0; break;
                    case Unit_Area.yd2: result = value * 0.111111; break;
                    case Unit_Area.ac: result = value * 0.000023; break;
                    default: break;
                }
                break;
            case Unit_Area.yd2:
                switch (targetType)
                {
                    case Unit_Area.m2: result = value * 0.836127; break;
                    case Unit_Area.a: result = value * 0.008361; break;
                    case Unit_Area.ha: result = value * 0.000084; break;
                    case Unit_Area.km2: result = value * 8.3613e-7; break;
                    case Unit_Area.ft2: result = value * 9.0; break;
                    case Unit_Area.yd2: result = value * 1.0; break;
                    case Unit_Area.ac: result = value * 0.000207; break;
                    default: break;
                }
                break;
            case Unit_Area.ac:
                switch (targetType)
                {
                    case Unit_Area.m2: result = value * 4046.85642; break;
                    case Unit_Area.a: result = value * 40.468564; break;
                    case Unit_Area.ha: result = value * 0.404686; break;
                    case Unit_Area.km2: result = value * 0.004047; break;
                    case Unit_Area.ft2: result = value * 43560.0; break;
                    case Unit_Area.yd2: result = value * 4840.0; break;
                    case Unit_Area.ac: result = value * 1.0; break;
                    default: break;
                }
                break;
            default:
                break;
        }
        return result;
    }
    double ConvertShoes(Unit_Shoes sourceType, Unit_Shoes targetType, double value)
    {
        double result = 0.0;
        switch (sourceType)
        {
            case Unit_Shoes.Asia:
                switch (targetType)
                {
                    case Unit_Shoes.Asia: result = value * 1.0; break;
                    case Unit_Shoes.US_M: result = (value / 10.0) - 18.0; break;
                    case Unit_Shoes.US_W: result = (value / 10.0) - 17.0; break;
                    case Unit_Shoes.US_B: result = (value / 10.0) - 19.0; break;
                    default:
                        break;
                }
                break;
            case Unit_Shoes.US_M:
                switch (targetType)
                {
                    case Unit_Shoes.Asia: result = (value + 18.0) * 10.0; break;
                    case Unit_Shoes.US_M: result = value * 1.0; break;
                    case Unit_Shoes.US_W: result = value + 1.0; break;
                    case Unit_Shoes.US_B: result = value - 1.0; break;
                    default:
                        break;
                }
                break;
            case Unit_Shoes.US_W:
                switch (targetType)
                {
                    case Unit_Shoes.Asia: result = (value + 17.0) * 10.0; break;
                    case Unit_Shoes.US_M: result = value - 1.0; break;
                    case Unit_Shoes.US_W: result = value * 1.0; break;
                    case Unit_Shoes.US_B: result = value - 2.0; break;
                    default:
                        break;
                }
                break;
            case Unit_Shoes.US_B:
                switch (targetType)
                {
                    case Unit_Shoes.Asia: result = (value + 19.0) * 10.0; break;
                    case Unit_Shoes.US_M: result = value + 1.0; break;
                    case Unit_Shoes.US_W: result = value + 2.0; break;
                    case Unit_Shoes.US_B: result = value * 1.0; break;
                    default:
                        break;
                }
                break;
            default:
                break;
        }
        return result;
    }
    double ConvertGas(Unit_Gas sourceType, Unit_Gas Unit_Gas, double value)
    {
        double result = 0.0;
        switch (sourceType)
        {
            case Unit_Gas.mpg:
                switch (Unit_Gas)
                {
                    case Unit_Gas.mpg: result = value * 1.0; break;
                    case Unit_Gas.km_L: result = (value * 1.609344) / 3.7854118; break;
                    default:
                        break;
                }
                break;
            case Unit_Gas.km_L:
                switch (Unit_Gas)
                {
                    case Unit_Gas.mpg: result = (value * 3.7854118) / 1.609344; break;
                    case Unit_Gas.km_L: result = value * 1.0; break;
                    default:
                        break;
                }
                break;
                break;
            default:
                break;
        }
        return result;
    }
    double ConvertCustom(double value)
    {
        double result = 0.0;
        switch (selectedIndex_Custom)
        {
            case Unit_Custom.MUL:
                result = value * value_Ratio;
                break;
            case Unit_Custom.DIV:
                result = value / value_Ratio;
                break;
            default:
                break;
        }
        return result;
    }

    #endregion

    #region Events
    public void Click_Exit()
    {
        Application.Quit();
    }
    public void SelectedIndex_Type()
    {
        ChangeType((Type)dpType.value);
    }
    public void SelectedIndex_Unit_A()
    {
        selectedIndex_A = dpUnitA.value;
    }
    public void SelectedIndex_Unit_B()
    {
        selectedIndex_B = dpUnitB.value;
    }
    public void SelectedIndex_Custom()
    {
        selectedIndex_Custom = (Unit_Custom)dpCustom.value;
    }
    public void ChangedValue_Unit_A()
    {
        double result = 0.0;
        if (double.TryParse(txtUnitA.text, out result))
        {
            value_UnitA = result;
        }
     
    }
    public void ChangedValue_Unit_B()
    {
        double result = 0.0;
        if (double.TryParse(txtUnitB.text, out result))
        {
            value_UnitB = result;
        }
    }
    public void ChangedValue_Ratio()
    {
        double result = 0.0;
        if (double.TryParse(txtRatio.text, out result))
        {
            value_Ratio = result;
        }
    }
    public void Click_Convert()
    {
        try
        {
            double value = ConvertUnit(selectedType, selectedIndex_A, selectedIndex_B, value_UnitA);
            value_UnitB = value;
            txtUnitB.text = value_UnitB.ToString().ToLower();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.Message);
        }
    }
    public void Click_Reset()
    {
        txtUnitA.text = string.Empty;
        txtUnitB.text = string.Empty;
        value_UnitA = 0.0;
        value_UnitB = 0.0;
    }
    public void Click_Swap()
    {
        txtUnitA.text = string.Empty;
        txtUnitB.text = string.Empty;
        

        //double tmpD_A = value_UnitB;
        //double tmpD_B = value_UnitA;
        //value_UnitA = tmpD_A;
        //value_UnitB = tmpD_B;
        //string textA = value_UnitA.ToString();
        //string textB = value_UnitB.ToString();
        //txtUnitA.text = textA;
        //txtUnitB.text = textB;

        int tmpI = selectedIndex_A;
        selectedIndex_A = selectedIndex_B;
        selectedIndex_B = tmpI;
        dpUnitA.value = selectedIndex_A;
        dpUnitB.value = selectedIndex_B;
    }
    #endregion

}
