import React from 'react'

type Props = {
  token: string;
}

export const OwnerMain = ({ token }: Props) => {
  return (
    <div>
      owner_main
      {token}
    </div>
  )
}
