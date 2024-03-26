import React from 'react'

export default function SelectProvinces({ provinces }) {
  return (
    <div>{provinces.map((value) => value.name)}</div>
  )
}
