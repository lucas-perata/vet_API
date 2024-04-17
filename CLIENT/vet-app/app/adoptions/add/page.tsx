import React from 'react'
import AdoptionForm from './AdoptionForm'

export default function page() {
  return (
    <div className='mx-auto max-w-6xl shadow-lg p-10 bg-white rounded-lg'>
      <h1 className="text-2xl font-extrabold tracking-tight">Creá el anuncio de adopción</h1>
      <div>
        <AdoptionForm></AdoptionForm>
      </div>
    </div>
  )
}

