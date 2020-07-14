import React, { Component } from 'react';
import BreadInventory from './BreadInventory';
import BakersTable from './BakersTable';
import axios from 'axios';
import { connect } from 'react-redux';

class Home extends Component {
  static displayName = Home.name;

  fetchBakers = async () => {
    const response = await axios.get('api/baker');
    this.props.dispatch({ type: 'SET_BAKERS', payload: response.data });
  }

  render() {
    return (
      <>
        <h1>Welcome To Blaine's Bakery</h1>
        <BreadInventory fetchBakers={this.fetchBakers} />
        <br />
        <BakersTable fetchBakers={this.fetchBakers} />
      </>
    );
  }
}

export default connect()(Home);