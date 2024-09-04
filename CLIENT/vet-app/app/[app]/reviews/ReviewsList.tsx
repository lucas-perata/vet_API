"use client"
import React, { useState } from 'react'
import ReviewsDash from '../dashboard/dashboardCards/ReviewsDash';

function ReviewsList(props) {
  return (
    <div><ReviewsDash data={props.data.data.data} /></div>
  )
}

export default ReviewsList
