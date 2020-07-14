import React, { Component } from 'react';
import axios from 'axios';
import { connect } from 'react-redux';

class BreadInventory extends Component {
   state = {
      loading: true,
      errors: [],
      successMessage: null,
      newBread: {
         name: '',
         breadType: 'sourdough',
         inventory: 1,
         description: '',
         bakedById: '',
      }
   }

   componentDidMount = () => {
      this.fetchData();
   }

   renderTable = () => {
      return (
         <table className='table table-striped' aria-labelledby="tabelLabel">
            <thead>
               <tr>
                  <th>Bread</th>
                  <th>Type</th>
                  <th>Description</th>
                  <th>Baked By</th>
                  <th>Quantity</th>
                  <th></th>
               </tr>
            </thead>
            <tbody>
               {this.props.breadInventory.map(bread =>
                  <tr key={`bread-row-${bread.id}`}>
                     <td>{bread.name}</td>
                     <td>{bread.breadType}</td>
                     <td>{bread.description}</td>
                     <td>{bread.bakedBy.name}</td>
                     <td>{bread.inventory}</td>
                     <td>
                        <button onClick={() => this.bake(bread.id)} className='btn btn-sm btn-info'>bake</button>
                        <button onClick={() => this.sell(bread.id)} className='btn btn-sm btn-info ml-1 mr-1'>eat</button>
                        <button onClick={() => this.delete(bread.id)} className='btn btn-sm btn-danger'>del</button>
                     </td>
                  </tr>
               )}
            </tbody>
         </table>
      );
   }

   addBread = async () => {
      try {
         await axios.post('api/bread', this.state.newBread);
         this.fetchData();
         this.setState({ 
            errors: [],
            success: 'Successfully added bread!'
         });
      } catch (err) {
         console.log(err);
         if (err.response.status === 400) {
            // validation errors
            this.setState({errors: err.response.data.errors, successMessage: null});
         }
      }
   }

   renderMessages = () => {
      /*
         Look into the local state to see if we have any errors
         that are derived from the backend validation, and display them
      */
      const errors = [];
      if (this.state.errors) {
         for (let err in this.state.errors) {
            // check for special case errors for human readability
            // .NET throws a weird validation error for database foreign key 
            // violations starting with $. for the field name... weird.
            if (err === '$.bakedById') {
               errors.push(<li>Invalid Baker ID</li>)
            } else {
               errors.push(<li>{this.state.errors[err]}</li>);
            }
         }
      }

      if (errors.length > 0) {
         return (
         <div className={'alert alert-danger'}>
            <p>The following errors prevented a successful save:</p>
            <ul>{errors}</ul>
         </div>);
      }

      if (this.state.successMessage !== null) {
         return (
            <p className={'alert alert-success'}>
               {this.state.successMessage}
            </p>
         );
      }

      return null;
   }

   render() {
      let contents = this.state.loading
         ? <p><em>Loading...</em></p>
         : this.renderTable();

      return (
         <>
            <h2 id="tableLabel" >Bread Inventory</h2>
            {this.renderMessages()}
            <div className="form-group row ml-0 mr-0">
               <input
                  placeholder={'bread name'}
                  className={"form-control col-2"}
                  value={this.state.newBread.name}
                  onChange={(e) => this.setState({ newBread: { ...this.state.newBread, name: e.target.value } })}
               />
               <select className={"form-control col-2"} value={this.state.newBread.breadType} onChange={(e) => this.setState({ newBread: { ...this.state.newBread, breadType: e.target.value } })}>
                  <option value='sourdough'>Sourdough</option>
                  <option value='rye'>Rye</option>
                  <option value='focaccia'>Focaccia</option>
                  <option value='white'>White</option>
               </select>
               <input className={"form-control col-2"}
                  placeholder={'description'}
                  value={this.state.newBread.description} 
                  onChange={(e) => this.setState({ newBread: { ...this.state.newBread, description: e.target.value } })} />
               <input className={"form-control col-1"} type='number' value={this.state.newBread.inventory} onChange={(e) => this.setState({ newBread: { ...this.state.newBread, inventory: Number(e.target.value) } })} />
               <select className={"form-control col-2"} value={this.state.newBread.bakedById} onChange={(e) => this.setState({ newBread: { ...this.state.newBread, bakedById: Number(e.target.value) } })}>
                  <option>Select Baker</option>
                  {this.props.bakers.map(baker => <option value={baker.id} key={`select-baker=${baker.id}`}>{baker.name}</option>)}
               </select>
               <button className={"form-control btn btn-primary col-2 ml-2"} onClick={this.addBread}>Add Bread</button>
            </div>
            <hr/>
            <p>Here's what we have in stock now!</p>
            {contents}
         </>
      );
   }

   delete = async (id) => {
      await axios.delete(`api/bread/${id}`);
      this.fetchData();
      this.setState({
         errors: [],
         success: `Successfully removed bread`
      });
   }

   bake = async (id) => {
      await axios.put(`api/bread/${id}/bake`);
      this.fetchData();
   }
   sell = async (id) => {
      await axios.put(`api/bread/${id}/sell`);
      this.fetchData();
   }

   fetchData = async () => {
      const response = await axios.get('api/bread');
      // this.setState({ breadInventory: response.data, loading: false });
      this.props.dispatch({ type: 'SET_BREAD', payload: response.data });
      this.props.fetchBakers();
   }
}

const mapStateToProps = (state) => ({
   breadInventory: state.breadInventory,
   bakers: state.bakers,
});
export default connect(mapStateToProps)(BreadInventory);