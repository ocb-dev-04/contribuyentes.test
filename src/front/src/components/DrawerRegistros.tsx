import React, { useEffect } from "react";
import {
  Drawer,
  Box,
  Typography,
  List,
  ListItem,
  ListItemText,
} from "@mui/material";
import { useRegistros } from "../hooks/useRegistros";
import LoadingSpinner from "./LoadingSpinner";
import AlertMessage from "./AlertMessage";
import Pagination from "./Pagination";

type Props = {
  open: boolean;
  contribuyenteId: string | null;
  onClose: () => void;
};

const DrawerRegistros: React.FC<Props> = ({ open, contribuyenteId, onClose }) => {
  const id = contribuyenteId ?? "";

  const { data, page, setPage, totalPages, isLastPage, loading, totalSum } =
    useRegistros(id);

  useEffect(() => {
    if (id) setPage(1);
  }, [id, setPage]);

  return (
    <Drawer
      anchor="right"
      open={open}
      onClose={onClose}
      PaperProps={{ sx: { width: "30vw", maxWidth: 520 } }}
    >
      <Box sx={{ p: 2, display: "flex", flexDirection: "column", height: "100%" }}>
        <Typography variant="h6" mb={1}>Registros</Typography>
        <Typography variant="body2" color="text.secondary" mb={2}>
          Contribuyente: {contribuyenteId ?? "—"}
        </Typography>

        <Typography variant="subtitle2" color="text.secondary">Total ITBIS</Typography>
        <Typography variant="h5" mb={2}>
          {totalSum.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}
        </Typography>

        {isLastPage && (
          <AlertMessage severity="warning" message="Ya estás en la última página." />
        )}

        <Box sx={{ flex: "1 1 auto", minHeight: 0, overflow: "auto", mb: 2 }}>
          {loading ? (
            <LoadingSpinner />
          ) : data.length === 0 ? (
            <Typography variant="body2" color="text.secondary">
              No hay registros para mostrar.
            </Typography>
          ) : (
            <List dense>
              {data.map((r, i) => (
                <ListItem key={`${r.ncf}-${i}`} divider>
                  <ListItemText
                    primary={r.ncf}
                    secondary={
                      <>
                        <div>Monto: {r.monto.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</div>
                        <div>ITBIS 18%: {r.itebis18.toLocaleString(undefined, { minimumFractionDigits: 2, maximumFractionDigits: 2 })}</div>
                      </>
                    }
                  />
                </ListItem>
              ))}
            </List>
          )}
        </Box>

        <Pagination page={page} totalPages={totalPages} onChange={setPage} />
      </Box>
    </Drawer>
  );
};

export default DrawerRegistros;
