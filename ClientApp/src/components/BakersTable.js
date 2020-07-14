import React, { Component } from 'react';
import axios from 'axios';
import { connect } from 'react-redux';

class BakerTable extends Component {
   state = {
      isLoading: true,
      newBaker: {
          name: ''
      }
   }

   componentDidMount = () => {
      this.props.fetchBakers();
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
               {this.props.bakers.map(baker =>
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
         <>
            <h2 id="tableLabel">Bakers</h2>
            <div className="form-group row ml-0">
                <input 
                    value={this.state.newBaker.name} 
                    onChange={(event) => this.setState({ newBaker: { ...this.state.newBaker, name: event.target.value}})}
                    className={'form-control col-3'}
                />
                <button onClick={this.submitBaker} className={'btn btn-primary col-2'}>Add Baker</button>
            </div>
            {contents}
         </>
      );
   }

   deleteBaker = async (id) => {
       await axios.delete(`api/baker/${id}`)
       this.props.fetchBakers();
   }

   submitBaker = async () => {
       await axios.post('api/baker', this.state.newBaker);
       this.setState({ newBaker: { ...this.state.newBaker, name: '' }});
       this.props.fetchBakers();
   }
}

const mapStateToProps = (state) => ({bakers: state.bakers});
export default connect(mapStateToProps)(BakerTable);