import React, { Component } from 'react';
import axios from 'axios';

export default class BakerTable extends Component {
   state = {
      isLoading: true,
      bakers: [],
      newBaker: {
          name: ''
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
                  <th>ID</th>
                  <th>Baker Name</th>
                  <th>Bread Count</th>
                  <th></th>
               </tr>
            </thead>
            <tbody>
               {this.state.bakers.map(baker =>
                  <tr key={`baker-row-baker.id`}>
                     <td>{baker.id}</td>
                     <td>{baker.name}</td>
                     <td>{baker.breadCount}</td>
                     <td><button onClick={() => this.deleteBaker(baker.id)}>Delete</button></td>
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
            <h1 id="tableLabel" >Bakers</h1>
            <input value={this.state.newBaker.name} onChange={(event) => this.setState({ newBaker: { ...this.state.newBaker, name: event.target.value}})} />
            <button onClick={this.submitBaker}>Add Baker</button>
            {contents}
         </div>
      );
   }

   deleteBaker = async (id) => {
       await axios.delete(`api/baker/${id}`)
       this.fetchData();
   }

   submitBaker = async () => {
       await axios.post('api/baker', this.state.newBaker);
       this.setState({ newBaker: { ...this.state.newBaker, name: '' }});
       this.fetchData();
   }

   fetchData = async () => {
      const response = await axios.get('api/baker');
      this.setState({ bakers: response.data, loading: false });
   }
}
