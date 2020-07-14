import React, { Component } from 'react';
import axios from 'axios';

export default class BreadInventory extends Component {
   state = {
      isLoading: true,
      breadInventory: [],
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
                  <th>Description</th>
                  <th>Quantity</th>
                  <th></th>
               </tr>
            </thead>
            <tbody>
               {this.state.breadInventory.map(bread =>
                  <tr key={`bread-row-bread.id`}>
                     <td>{bread.name}</td>
                     <td>{bread.description}</td>
                     <td>{bread.inventory}</td>
                     <td>
                        <button onClick={() => this.bake(bread.id)}>bake</button> 
                        <button onClick={() => this.sell(bread.id)}>eat</button>
                     </td>
                  </tr>
               )}
            </tbody>
         </table>
      );
   }

   render() {
      let contents = this.state.loading
         ? <p><em>Loading...</em></p>
         : this.renderTable();

      return (
         <div>
            <h1 id="tableLabel" >Bread Inventory</h1>
            <p>Here's what we have in stock now!</p>
            {contents}
         </div>
      );
   }

   bake = async (id) => {
      const response = await axios.put(`api/bread/${id}/bake`);
      this.fetchData();
   }   
   sell = async (id) => {
      const response = await axios.put(`api/bread/${id}/sell`);
      this.fetchData();
   }

   fetchData = async () => {
      const response = await axios.get('api/bread');
      this.setState({ breadInventory: response.data, loading: false });
   }
}
