import React, { Component } from 'react';
import axios from 'axios';

export default class BreadInventory extends Component {
   state = {
      isLoading: true,
      breadInventory: [],
      newBread: {
         name: 'bread name',
         breadType: 'sourdough',
         inventory: 1,
         description: 'description',
         bakedById: null,
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
               {this.state.breadInventory.map(bread =>
                  <tr key={`bread-row-bread.id`}>
                     <td>{bread.name}</td>
                     <td>{bread.breadType}</td>
                     <td>{bread.description}</td>
                     <td>{bread.bakedBy.name}</td>
                     <td>{bread.inventory}</td>
                     <td>
                        <button onClick={() => this.bake(bread.id)}>bake</button> 
                        <button onClick={() => this.sell(bread.id)}>eat</button>
                        <button onClick={() => this.delete(bread.id)}>del</button>
                     </td>
                  </tr>
               )}
            </tbody>
         </table>
      );
   }

   addBread = async () => {
      const response = await axios.post('api/bread', this.state.newBread);
      this.fetchData();
   }

   render() {
      let contents = this.state.loading
         ? <p><em>Loading...</em></p>
         : this.renderTable();

      return (
         <div>
            <h1 id="tableLabel" >Bread Inventory</h1>
            <input value={this.state.newBread.name} onChange={(e) => this.setState({ newBread: { ...this.state.newBread, name: e.target.value }})} />
            <select value={this.state.newBread.breadType} onChange = {(e) => this.setState({ newBread: { ...this.state.newBread, breadType: e.target.value }})}>
               <option value='sourdough'>Sourdough</option>
               <option value='rye'>Rye</option>
               <option value='focaccia'>Focaccia</option>
               <option value='white'>White</option>
            </select>
            <input value={this.state.newBread.description} onChange={(e) => this.setState({ newBread: { ...this.state.newBread, description: e.target.value }})} />
            <input type='number' value={this.state.newBread.inventory} onChange={(e) => this.setState({ newBread: { ...this.state.newBread, inventory: Number(e.target.value) }})} />
            <input type='number' value={this.state.newBread.bakedById} onChange={(e) => this.setState({ newBread: { ...this.state.newBread, bakedById: Number(e.target.value) }})} />
            <button onClick={this.addBread}>Add Bread</button>

            <p>Here's what we have in stock now!</p>
            {contents}
         </div>
      );
   }

   delete = async (id) => {
      const response = await axios.delete(`api/bread/${id}`);
      this.fetchData();
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
