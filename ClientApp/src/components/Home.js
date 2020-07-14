import React, { Component } from 'react';
import BreadInventory from './BreadInventory';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <>
        <h1>Welcome To Blaine's Bakery</h1>
        <BreadInventory />
      </>
    );
  }
}
