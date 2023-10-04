import React, { Component } from 'react';
import authService from '../../components/api-authorization/AuthorizeService';

export class EmployeeCreate extends Component {
  static displayName = EmployeeCreate.name;

  constructor(props) {
    super(props);
    this.state = { fullName: '',birthdate: '',tin: '',typeId: 1, loading: false,loadingSave:false, Fields: [] };
  }

  componentDidMount() {
      if (this.props.onComponentMounted) {
          this.props.onComponentMounted(this); //register this input in the form
      }
  }

  //validation function
  isValid(input) {
    //check required field
    if (input.getAttribute('required') != null && input.value === "") {
        input.classList.add('error'); //add class error
        input.nextSibling.textContent = this.props.messageRequired; // show error message
        return false;
    }
    else {
        input.classList.remove('error');
        input.nextSibling.textContent = "";
    }    
    return true;
  }

  //register input controls
  register(field){
    var s = this.state.Fields;
    s.push(field);
    this.setState({
        Fields: s
    })
  }

  handleChange(event) {
    this.setState({ [event.target.name] : event.target.value});
  }

  handleSubmit(e){
      e.preventDefault();
      //Validate fields here
      var validForm = true;
      if (this.state.fullName.length === 0 || this.state.birthdate.length === 0
          || this.state.tin.length === 0 || this.state.typeId.length === 0) {
          validForm = false;
      }

      // If valid proceed save.
      if (validForm) {
          if (window.confirm("Are you sure you want to save?")) {
              this.saveEmployee();
          }
      } else {
          alert("All fields are required!");
      }
  }

  render() {

    let contents = this.state.loading
    ? <p><em>Loading...</em></p>
    : <div>
    <form noValidate>
<div className='form-row'>
<div className='form-group col-md-6'>
  <label htmlFor='inputFullName4'>Full Name: *</label>
                        <input type='text' className='form-control' id='inputFullName4' isrequired="true" onChange={this.handleChange.bind(this)} name="fullName" value={this.state.fullName} placeholder='Full Name' required={this.props.isrequired} />
</div>
<div className='form-group col-md-6'>
  <label htmlFor='inputBirthdate4'>Birthdate: *</label>
                        <input type='date' className='form-control' id='inputBirthdate4' isrequired="true" onChange={this.handleChange.bind(this)} name="birthdate" value={this.state.birthdate} placeholder='Birthdate' required={this.props.isrequired}/>
</div>
</div>
<div className="form-row">
<div className='form-group col-md-6'>
  <label htmlFor='inputTin4'>TIN: *</label>
                        <input type='text' className='form-control' id='inputTin4' isrequired="true" onChange={this.handleChange.bind(this)} value={this.state.tin} name="tin" placeholder='TIN' required={this.props.isrequired} />
</div>
<div className='form-group col-md-6'>
  <label htmlFor='inputEmployeeType4'>Employee Type: *</label>
                        <select id='inputEmployeeType4' isrequired="true" onChange={this.handleChange.bind(this)} value={this.state.typeId} name="typeId" className='form-control' required={this.props.isrequired}>
    <option value='1'>Regular</option>
    <option value='2'>Contractual</option>
  </select>
</div>
</div>
<button type="submit" onClick={this.handleSubmit.bind(this)} disabled={this.state.loadingSave} className="btn btn-primary mr-2">{this.state.loadingSave?"Loading...": "Save"}</button>
<button type="button" onClick={() => this.props.history.push("/employees/index")} className="btn btn-primary">Back</button>
</form>
</div>;

    return (
        <div>
        <h1 id="tabelLabel" >Employee Create</h1>
        <p>All fields are required</p>
        {contents}
      </div>
    );
  }

  async saveEmployee() {
    this.setState({ loadingSave: true });
    const token = await authService.getAccessToken();
    const requestOptions = {
        method: 'POST',
        headers: !token ? {} : { 'Authorization': `Bearer ${token}`,'Content-Type': 'application/json' },
        body: JSON.stringify(this.state)
    };
    const response = await fetch('api/employees',requestOptions);

    if(response.status === 201){
        this.setState({ loadingSave: false });
        alert("Employee successfully saved");
        this.props.history.push("/employees/index");
    }
    else{
        alert("There was an error occured.");
    }
  }

}
