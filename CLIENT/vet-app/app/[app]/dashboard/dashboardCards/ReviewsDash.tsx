import React from 'react'
import { Card, CardContent, CardHeader, CardTitle } from '@/components/ui/card'
import {
  Table,
  TableBody,
  TableCell,
  TableHead,
  TableHeader,
  TableRow,
} from "@/components/ui/table"
import { Review } from '@/types'


function ReviewsDash(data) {
  return (
    <Card className="h-[35vh] w-[35vw]">
      <CardHeader>
        <CardTitle className="text-xl">Últimas reseñas</CardTitle>
      </CardHeader>
      <CardContent>
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead>Fecha</TableHead>
              <TableHead>Estrellas</TableHead>
              <TableHead>Descripción</TableHead>
              <TableHead>Status</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {
              data.data.map((review: Review) => (
                <TableRow>
                  <TableCell>{review.date}</TableCell>
                  <TableCell>{review.stars}</TableCell>
                  <TableCell>{review.body}</TableCell>
                  {/* TODO: Status API */}
                  <TableCell>Pending</TableCell>
                </TableRow>
              ))
            }
          </TableBody>
        </Table>

      </CardContent>
    </Card>
  )
}

export default ReviewsDash
