import { AuthWrapper } from '@/components/AuthWraper'
import React from 'react'

export default function someProtectedPage () {
  return (
    <AuthWrapper>
        <div>some-protected-page</div>
    </AuthWrapper>
  )
}

