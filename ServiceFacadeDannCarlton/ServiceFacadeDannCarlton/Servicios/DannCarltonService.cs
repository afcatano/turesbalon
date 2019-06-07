﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------



[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(Namespace="http://xmlns.touresbalon.com/DannCarltonService", ConfigurationName="DannCarltonService")]
public interface DannCarltonService
{
    
    // CODEGEN: Se está generando un contrato de mensaje, ya que la operación GetDannBranch no es RPC ni está encapsulada en un documento.
    [System.ServiceModel.OperationContractAttribute(Action="http://xmlns.touresbalon.com/DannCarltonService/GetDannBranch", ReplyAction="*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    GetDannBranchResponse1 GetDannBranch(GetDannBranchRequest1 request);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://xmlns.touresbalon.com/DannCarltonService/GetDannBranch", ReplyAction="*")]
    System.Threading.Tasks.Task<GetDannBranchResponse1> GetDannBranchAsync(GetDannBranchRequest1 request);
    
    // CODEGEN: Se está generando un contrato de mensaje, ya que la operación GetRoomsByBranch no es RPC ni está encapsulada en un documento.
    [System.ServiceModel.OperationContractAttribute(Action="http://xmlns.touresbalon.com/DannCarltonService/GetRoomsByBranch", ReplyAction="*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    GetRoomsByBranchResponse1 GetRoomsByBranch(GetRoomsByBranchRequest1 request);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://xmlns.touresbalon.com/DannCarltonService/GetRoomsByBranch", ReplyAction="*")]
    System.Threading.Tasks.Task<GetRoomsByBranchResponse1> GetRoomsByBranchAsync(GetRoomsByBranchRequest1 request);
    
    // CODEGEN: Se está generando un contrato de mensaje, ya que la operación GetBookedRoomsByBranch no es RPC ni está encapsulada en un documento.
    [System.ServiceModel.OperationContractAttribute(Action="http://xmlns.touresbalon.com/DannCarltonService/GetBookedRoomsByBranch", ReplyAction="*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    GetBookedRoomsByBranchResponse1 GetBookedRoomsByBranch(GetBookedRoomsByBranchRequest1 request);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://xmlns.touresbalon.com/DannCarltonService/GetBookedRoomsByBranch", ReplyAction="*")]
    System.Threading.Tasks.Task<GetBookedRoomsByBranchResponse1> GetBookedRoomsByBranchAsync(GetBookedRoomsByBranchRequest1 request);
    
    // CODEGEN: Se está generando un contrato de mensaje, ya que la operación BookRoom no es RPC ni está encapsulada en un documento.
    [System.ServiceModel.OperationContractAttribute(Action="http://xmlns.touresbalon.com/DannCarltonService/BookRoom", ReplyAction="*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    BookRoomResponse1 BookRoom(BookRoomRequest1 request);
    
    [System.ServiceModel.OperationContractAttribute(Action="http://xmlns.touresbalon.com/DannCarltonService/BookRoom", ReplyAction="*")]
    System.Threading.Tasks.Task<BookRoomResponse1> BookRoomAsync(BookRoomRequest1 request);
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.touresbalon.com/DannCarltonService")]
public partial class GetDannBranchRequest
{
    
    private string branchCodeField;
    
    private string branchNameField;
    
    private string cityNameField;
    
    private string ciiuCodeField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public string BranchCode
    {
        get
        {
            return this.branchCodeField;
        }
        set
        {
            this.branchCodeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=1)]
    public string BranchName
    {
        get
        {
            return this.branchNameField;
        }
        set
        {
            this.branchNameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=2)]
    public string CityName
    {
        get
        {
            return this.cityNameField;
        }
        set
        {
            this.cityNameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=3)]
    public string CiiuCode
    {
        get
        {
            return this.ciiuCodeField;
        }
        set
        {
            this.ciiuCodeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://xmlns.touresbalon.com/DannCarltonService")]
public partial class GetBookedRoomsByBranchResult
{
    
    private System.DateTime bookDateField;
    
    private string branchNameField;
    
    private string branchCodeField;
    
    private string roomNameField;
    
    private string numberField;
    
    private int bedsField;
    
    private string descriptionField;
    
    private decimal priceField;
    
    private decimal vatField;
    
    private decimal discountField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="date", Order=0)]
    public System.DateTime BookDate
    {
        get
        {
            return this.bookDateField;
        }
        set
        {
            this.bookDateField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=1)]
    public string BranchName
    {
        get
        {
            return this.branchNameField;
        }
        set
        {
            this.branchNameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=2)]
    public string BranchCode
    {
        get
        {
            return this.branchCodeField;
        }
        set
        {
            this.branchCodeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=3)]
    public string RoomName
    {
        get
        {
            return this.roomNameField;
        }
        set
        {
            this.roomNameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=4)]
    public string Number
    {
        get
        {
            return this.numberField;
        }
        set
        {
            this.numberField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=5)]
    public int Beds
    {
        get
        {
            return this.bedsField;
        }
        set
        {
            this.bedsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=6)]
    public string Description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=7)]
    public decimal Price
    {
        get
        {
            return this.priceField;
        }
        set
        {
            this.priceField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=8)]
    public decimal Vat
    {
        get
        {
            return this.vatField;
        }
        set
        {
            this.vatField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=9)]
    public decimal Discount
    {
        get
        {
            return this.discountField;
        }
        set
        {
            this.discountField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://xmlns.touresbalon.com/DannCarltonService")]
public partial class GetRoomsByBranchResult
{
    
    private int idField;
    
    private string roomNameField;
    
    private string numberField;
    
    private int bedsField;
    
    private string descriptionField;
    
    private int branchIdField;
    
    private decimal priceField;
    
    private decimal vatField;
    
    private decimal discountField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public int Id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=1)]
    public string RoomName
    {
        get
        {
            return this.roomNameField;
        }
        set
        {
            this.roomNameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=2)]
    public string Number
    {
        get
        {
            return this.numberField;
        }
        set
        {
            this.numberField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=3)]
    public int Beds
    {
        get
        {
            return this.bedsField;
        }
        set
        {
            this.bedsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=4)]
    public string Description
    {
        get
        {
            return this.descriptionField;
        }
        set
        {
            this.descriptionField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=5)]
    public int BranchId
    {
        get
        {
            return this.branchIdField;
        }
        set
        {
            this.branchIdField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=6)]
    public decimal Price
    {
        get
        {
            return this.priceField;
        }
        set
        {
            this.priceField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=7)]
    public decimal Vat
    {
        get
        {
            return this.vatField;
        }
        set
        {
            this.vatField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=8)]
    public decimal Discount
    {
        get
        {
            return this.discountField;
        }
        set
        {
            this.discountField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://xmlns.touresbalon.com/DannCarltonService")]
public partial class GetDannBranchResult
{
    
    private int idField;
    
    private string branchCodeField;
    
    private string branchNameField;
    
    private decimal starsField;
    
    private string addressField;
    
    private string phoneField;
    
    private string cityField;
    
    private string ciiuCodeField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public int Id
    {
        get
        {
            return this.idField;
        }
        set
        {
            this.idField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=1)]
    public string BranchCode
    {
        get
        {
            return this.branchCodeField;
        }
        set
        {
            this.branchCodeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=2)]
    public string BranchName
    {
        get
        {
            return this.branchNameField;
        }
        set
        {
            this.branchNameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=3)]
    public decimal Stars
    {
        get
        {
            return this.starsField;
        }
        set
        {
            this.starsField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=4)]
    public string Address
    {
        get
        {
            return this.addressField;
        }
        set
        {
            this.addressField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=5)]
    public string Phone
    {
        get
        {
            return this.phoneField;
        }
        set
        {
            this.phoneField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=6)]
    public string City
    {
        get
        {
            return this.cityField;
        }
        set
        {
            this.cityField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=7)]
    public string CiiuCode
    {
        get
        {
            return this.ciiuCodeField;
        }
        set
        {
            this.ciiuCodeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://xmlns.touresbalon.com/DannCarltonService")]
public partial class Status
{
    
    private string errorCodeField;
    
    private string errorDescriptionField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public string ErrorCode
    {
        get
        {
            return this.errorCodeField;
        }
        set
        {
            this.errorCodeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=1)]
    public string ErrorDescription
    {
        get
        {
            return this.errorDescriptionField;
        }
        set
        {
            this.errorDescriptionField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.touresbalon.com/DannCarltonService")]
public partial class GetDannBranchResponse
{
    
    private Status statusField;
    
    private GetDannBranchResult[] getDannBranchResultField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public Status Status
    {
        get
        {
            return this.statusField;
        }
        set
        {
            this.statusField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("GetDannBranchResult", Order=1)]
    public GetDannBranchResult[] GetDannBranchResult
    {
        get
        {
            return this.getDannBranchResultField;
        }
        set
        {
            this.getDannBranchResultField = value;
        }
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
public partial class GetDannBranchRequest1
{
    
    [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.touresbalon.com/DannCarltonService", Order=0)]
    public GetDannBranchRequest GetDannBranchRequest;
    
    public GetDannBranchRequest1()
    {
    }
    
    public GetDannBranchRequest1(GetDannBranchRequest GetDannBranchRequest)
    {
        this.GetDannBranchRequest = GetDannBranchRequest;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
public partial class GetDannBranchResponse1
{
    
    [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.touresbalon.com/DannCarltonService", Order=0)]
    public GetDannBranchResponse GetDannBranchResponse;
    
    public GetDannBranchResponse1()
    {
    }
    
    public GetDannBranchResponse1(GetDannBranchResponse GetDannBranchResponse)
    {
        this.GetDannBranchResponse = GetDannBranchResponse;
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.touresbalon.com/DannCarltonService")]
public partial class GetRoomsByBranchRequest
{
    
    private string branchCodeField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public string BranchCode
    {
        get
        {
            return this.branchCodeField;
        }
        set
        {
            this.branchCodeField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.touresbalon.com/DannCarltonService")]
public partial class GetRoomsByBranchResponse
{
    
    private Status statusField;
    
    private GetRoomsByBranchResult[] getRoomsByBranchResultField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public Status Status
    {
        get
        {
            return this.statusField;
        }
        set
        {
            this.statusField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("GetRoomsByBranchResult", Order=1)]
    public GetRoomsByBranchResult[] GetRoomsByBranchResult
    {
        get
        {
            return this.getRoomsByBranchResultField;
        }
        set
        {
            this.getRoomsByBranchResultField = value;
        }
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
public partial class GetRoomsByBranchRequest1
{
    
    [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.touresbalon.com/DannCarltonService", Order=0)]
    public GetRoomsByBranchRequest GetRoomsByBranchRequest;
    
    public GetRoomsByBranchRequest1()
    {
    }
    
    public GetRoomsByBranchRequest1(GetRoomsByBranchRequest GetRoomsByBranchRequest)
    {
        this.GetRoomsByBranchRequest = GetRoomsByBranchRequest;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
public partial class GetRoomsByBranchResponse1
{
    
    [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.touresbalon.com/DannCarltonService", Order=0)]
    public GetRoomsByBranchResponse GetRoomsByBranchResponse;
    
    public GetRoomsByBranchResponse1()
    {
    }
    
    public GetRoomsByBranchResponse1(GetRoomsByBranchResponse GetRoomsByBranchResponse)
    {
        this.GetRoomsByBranchResponse = GetRoomsByBranchResponse;
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.touresbalon.com/DannCarltonService")]
public partial class GetBookedRoomsByBranchRequest
{
    
    private string branchCodeField;
    
    private string roomNumberField;
    
    private System.DateTime checkInField;
    
    private System.DateTime checkOutField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public string BranchCode
    {
        get
        {
            return this.branchCodeField;
        }
        set
        {
            this.branchCodeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=1)]
    public string RoomNumber
    {
        get
        {
            return this.roomNumberField;
        }
        set
        {
            this.roomNumberField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="date", Order=2)]
    public System.DateTime CheckIn
    {
        get
        {
            return this.checkInField;
        }
        set
        {
            this.checkInField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="date", Order=3)]
    public System.DateTime CheckOut
    {
        get
        {
            return this.checkOutField;
        }
        set
        {
            this.checkOutField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.touresbalon.com/DannCarltonService")]
public partial class GetBookedRoomsByBranchResponse
{
    
    private Status statusField;
    
    private GetBookedRoomsByBranchResult[] getBookedRoomsByBranchResultField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public Status Status
    {
        get
        {
            return this.statusField;
        }
        set
        {
            this.statusField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("GetBookedRoomsByBranchResult", Order=1)]
    public GetBookedRoomsByBranchResult[] GetBookedRoomsByBranchResult
    {
        get
        {
            return this.getBookedRoomsByBranchResultField;
        }
        set
        {
            this.getBookedRoomsByBranchResultField = value;
        }
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
public partial class GetBookedRoomsByBranchRequest1
{
    
    [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.touresbalon.com/DannCarltonService", Order=0)]
    public GetBookedRoomsByBranchRequest GetBookedRoomsByBranchRequest;
    
    public GetBookedRoomsByBranchRequest1()
    {
    }
    
    public GetBookedRoomsByBranchRequest1(GetBookedRoomsByBranchRequest GetBookedRoomsByBranchRequest)
    {
        this.GetBookedRoomsByBranchRequest = GetBookedRoomsByBranchRequest;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
public partial class GetBookedRoomsByBranchResponse1
{
    
    [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.touresbalon.com/DannCarltonService", Order=0)]
    public GetBookedRoomsByBranchResponse GetBookedRoomsByBranchResponse;
    
    public GetBookedRoomsByBranchResponse1()
    {
    }
    
    public GetBookedRoomsByBranchResponse1(GetBookedRoomsByBranchResponse GetBookedRoomsByBranchResponse)
    {
        this.GetBookedRoomsByBranchResponse = GetBookedRoomsByBranchResponse;
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.touresbalon.com/DannCarltonService")]
public partial class BookRoomRequest
{
    
    private System.DateTime checkInField;
    
    private System.DateTime checkOutField;
    
    private int branchIdField;
    
    private int  roomIdField;
    
    private string guestFullNameField;
    
    private string guestDocumentNumberField;
    
    private int guestDocumentTypeIdField;
    
    private string sourceCompanyCodeField;
    
    private bool isCancelprocessField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="date", Order=0)]
    public System.DateTime CheckIn
    {
        get
        {
            return this.checkInField;
        }
        set
        {
            this.checkInField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="date", Order=1)]
    public System.DateTime CheckOut
    {
        get
        {
            return this.checkOutField;
        }
        set
        {
            this.checkOutField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=2)]
    public int BranchId
    {
        get
        {
            return this.branchIdField;
        }
        set
        {
            this.branchIdField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=3)]
    public int RoomId
    {
        get
        {
            return this.roomIdField;
        }
        set
        {
            this.roomIdField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=4)]
    public string GuestFullName
    {
        get
        {
            return this.guestFullNameField;
        }
        set
        {
            this.guestFullNameField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=5)]
    public string GuestDocumentNumber
    {
        get
        {
            return this.guestDocumentNumberField;
        }
        set
        {
            this.guestDocumentNumberField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=6)]
    public int GuestDocumentTypeId
    {
        get
        {
            return this.guestDocumentTypeIdField;
        }
        set
        {
            this.guestDocumentTypeIdField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=7)]
    public string SourceCompanyCode
    {
        get
        {
            return this.sourceCompanyCodeField;
        }
        set
        {
            this.sourceCompanyCodeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=8)]
    public bool IsCancelprocess
    {
        get
        {
            return this.isCancelprocessField;
        }
        set
        {
            this.isCancelprocessField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.6.1055.0")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://xmlns.touresbalon.com/DannCarltonService")]
public partial class BookRoomResponse
{
    
    private Status statusField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public Status Status
    {
        get
        {
            return this.statusField;
        }
        set
        {
            this.statusField = value;
        }
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
public partial class BookRoomRequest1
{
    
    [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.touresbalon.com/DannCarltonService", Order=0)]
    public BookRoomRequest BookRoomRequest;
    
    public BookRoomRequest1()
    {
    }
    
    public BookRoomRequest1(BookRoomRequest BookRoomRequest)
    {
        this.BookRoomRequest = BookRoomRequest;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
public partial class BookRoomResponse1
{
    
    [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://xmlns.touresbalon.com/DannCarltonService", Order=0)]
    public BookRoomResponse BookRoomResponse;
    
    public BookRoomResponse1()
    {
    }
    
    public BookRoomResponse1(BookRoomResponse BookRoomResponse)
    {
        this.BookRoomResponse = BookRoomResponse;
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface DannCarltonServiceChannel : DannCarltonService, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class DannCarltonServiceClient : System.ServiceModel.ClientBase<DannCarltonService>, DannCarltonService
{
    
    public DannCarltonServiceClient()
    {
    }
    
    public DannCarltonServiceClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public DannCarltonServiceClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public DannCarltonServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public DannCarltonServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    GetDannBranchResponse1 DannCarltonService.GetDannBranch(GetDannBranchRequest1 request)
    {
        return base.Channel.GetDannBranch(request);
    }
    
    public GetDannBranchResponse GetDannBranch(GetDannBranchRequest GetDannBranchRequest)
    {
        GetDannBranchRequest1 inValue = new GetDannBranchRequest1();
        inValue.GetDannBranchRequest = GetDannBranchRequest;
        GetDannBranchResponse1 retVal = ((DannCarltonService)(this)).GetDannBranch(inValue);
        return retVal.GetDannBranchResponse;
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    System.Threading.Tasks.Task<GetDannBranchResponse1> DannCarltonService.GetDannBranchAsync(GetDannBranchRequest1 request)
    {
        return base.Channel.GetDannBranchAsync(request);
    }
    
    public System.Threading.Tasks.Task<GetDannBranchResponse1> GetDannBranchAsync(GetDannBranchRequest GetDannBranchRequest)
    {
        GetDannBranchRequest1 inValue = new GetDannBranchRequest1();
        inValue.GetDannBranchRequest = GetDannBranchRequest;
        return ((DannCarltonService)(this)).GetDannBranchAsync(inValue);
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    GetRoomsByBranchResponse1 DannCarltonService.GetRoomsByBranch(GetRoomsByBranchRequest1 request)
    {
        return base.Channel.GetRoomsByBranch(request);
    }
    
    public GetRoomsByBranchResponse GetRoomsByBranch(GetRoomsByBranchRequest GetRoomsByBranchRequest)
    {
        GetRoomsByBranchRequest1 inValue = new GetRoomsByBranchRequest1();
        inValue.GetRoomsByBranchRequest = GetRoomsByBranchRequest;
        GetRoomsByBranchResponse1 retVal = ((DannCarltonService)(this)).GetRoomsByBranch(inValue);
        return retVal.GetRoomsByBranchResponse;
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    System.Threading.Tasks.Task<GetRoomsByBranchResponse1> DannCarltonService.GetRoomsByBranchAsync(GetRoomsByBranchRequest1 request)
    {
        return base.Channel.GetRoomsByBranchAsync(request);
    }
    
    public System.Threading.Tasks.Task<GetRoomsByBranchResponse1> GetRoomsByBranchAsync(GetRoomsByBranchRequest GetRoomsByBranchRequest)
    {
        GetRoomsByBranchRequest1 inValue = new GetRoomsByBranchRequest1();
        inValue.GetRoomsByBranchRequest = GetRoomsByBranchRequest;
        return ((DannCarltonService)(this)).GetRoomsByBranchAsync(inValue);
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    GetBookedRoomsByBranchResponse1 DannCarltonService.GetBookedRoomsByBranch(GetBookedRoomsByBranchRequest1 request)
    {
        return base.Channel.GetBookedRoomsByBranch(request);
    }
    
    public GetBookedRoomsByBranchResponse GetBookedRoomsByBranch(GetBookedRoomsByBranchRequest GetBookedRoomsByBranchRequest)
    {
        GetBookedRoomsByBranchRequest1 inValue = new GetBookedRoomsByBranchRequest1();
        inValue.GetBookedRoomsByBranchRequest = GetBookedRoomsByBranchRequest;
        GetBookedRoomsByBranchResponse1 retVal = ((DannCarltonService)(this)).GetBookedRoomsByBranch(inValue);
        return retVal.GetBookedRoomsByBranchResponse;
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    System.Threading.Tasks.Task<GetBookedRoomsByBranchResponse1> DannCarltonService.GetBookedRoomsByBranchAsync(GetBookedRoomsByBranchRequest1 request)
    {
        return base.Channel.GetBookedRoomsByBranchAsync(request);
    }
    
    public System.Threading.Tasks.Task<GetBookedRoomsByBranchResponse1> GetBookedRoomsByBranchAsync(GetBookedRoomsByBranchRequest GetBookedRoomsByBranchRequest)
    {
        GetBookedRoomsByBranchRequest1 inValue = new GetBookedRoomsByBranchRequest1();
        inValue.GetBookedRoomsByBranchRequest = GetBookedRoomsByBranchRequest;
        return ((DannCarltonService)(this)).GetBookedRoomsByBranchAsync(inValue);
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    BookRoomResponse1 DannCarltonService.BookRoom(BookRoomRequest1 request)
    {
        return base.Channel.BookRoom(request);
    }
    
    public BookRoomResponse BookRoom(BookRoomRequest BookRoomRequest)
    {
        BookRoomRequest1 inValue = new BookRoomRequest1();
        inValue.BookRoomRequest = BookRoomRequest;
        BookRoomResponse1 retVal = ((DannCarltonService)(this)).BookRoom(inValue);
        return retVal.BookRoomResponse;
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    System.Threading.Tasks.Task<BookRoomResponse1> DannCarltonService.BookRoomAsync(BookRoomRequest1 request)
    {
        return base.Channel.BookRoomAsync(request);
    }
    
    public System.Threading.Tasks.Task<BookRoomResponse1> BookRoomAsync(BookRoomRequest BookRoomRequest)
    {
        BookRoomRequest1 inValue = new BookRoomRequest1();
        inValue.BookRoomRequest = BookRoomRequest;
        return ((DannCarltonService)(this)).BookRoomAsync(inValue);
    }
}
