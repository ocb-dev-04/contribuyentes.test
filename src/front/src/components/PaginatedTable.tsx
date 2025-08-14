import {
  Table, TableBody, TableCell, TableContainer,
  TableHead, TableRow, Paper, Pagination, Box
} from "@mui/material";
import React from "react";

interface TableColumn<T> {
  key: keyof T | string;
  label: string;
  render?: (row: T) => React.ReactNode; // si viene, tiene prioridad
}

interface PaginatedTableProps<T> {
  columns: TableColumn<T>[];
  data: T[];
  totalPages: number;
  currentPage: number;
  onPageChange: (page: number) => void;
}

export function PaginatedTable<T>({
  columns,
  data,
  totalPages,
  currentPage,
  onPageChange
}: PaginatedTableProps<T>) {
  return (
    <Paper sx={{ display: "flex", flexDirection: "column", height: "100%" }}>
      <TableContainer sx={{ flex: "1 1 auto", maxHeight: "80%" }}>
        <Table stickyHeader>
          <TableHead>
            <TableRow>
              {columns.map((col) => (
                <TableCell key={String(col.key)}>{col.label}</TableCell>
              ))}
            </TableRow>
          </TableHead>

          <TableBody>
            {data.map((row, i) => (
              <TableRow key={i} hover>
                {columns.map((col) => (
                  <TableCell key={String(col.key)}>
                    {col.render
                      ? col.render(row) // 1) render custom si existe
                      : String((row as any)[col.key as keyof T]) // 2) valor por key
                    }
                  </TableCell>
                ))}
              </TableRow>
            ))}
            {data.length === 0 && (
              <TableRow>
                <TableCell colSpan={columns.length} align="center">
                  Sin datos
                </TableCell>
              </TableRow>
            )}
          </TableBody>
        </Table>
      </TableContainer>

      <Box sx={{ p: 2, display: "flex", justifyContent: "center" }}>
        <Pagination
          count={totalPages}
          page={currentPage}
          onChange={(_, page) => onPageChange(page)}
          color="primary"
        />
      </Box>
    </Paper>
  );
}
