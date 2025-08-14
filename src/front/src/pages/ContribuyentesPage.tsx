import { useEffect, useState } from "react";
import { PaginatedTable } from "../components/PaginatedTable";
import { getContribuyentes } from "../api/contribuyenteService";
import DrawerRegistros from "../components/DrawerRegistros";
import { Button } from "@mui/material";

interface Contribuyente {
  id: string;
  tipo: string;
  nombre: string;
  rncCedula: string;
  creadoEnUtc: string;
}

export default function ContribuyentesPage() {
  const [data, setData] = useState<Contribuyente[]>([]);
  const [page, setPage] = useState(1);
  const [totalPages, setTotalPages] = useState(1);

  const [drawerOpen, setDrawerOpen] = useState(false);
  const [selectedId, setSelectedId] = useState<string | null>(null);

  useEffect(() => {
    getContribuyentes(page).then((res) => {
      setData(res.data);
      setTotalPages(res.totalPages);
    });
  }, [page]);

  const handleOpenDrawer = (id: string) => {
    setSelectedId(id);
    setDrawerOpen(true);
  };

  const handleCloseDrawer = () => {
    setSelectedId(null);
    setDrawerOpen(false);
  };

  return (
    <div style={{ height: "100vh", padding: "20px" }}>
      <PaginatedTable
        columns={[
          { key: "id", label: "ID" },
          { key: "tipo", label: "Tipo" },
          { key: "nombre", label: "Nombre" },
          { key: "rncCedula", label: "RNC/Cédula" },
          { key: "creadoEnUtc", label: "Creado En" },
          {
            key: "acciones",
            label: "Acciones",
            render: (row: Contribuyente) => (
              <Button
                variant="outlined"
                size="small"
                onClick={() => handleOpenDrawer(row.id)}
              >
                Ver Registros
              </Button>
            ),
          },
        ]}
        data={data}
        totalPages={totalPages}
        currentPage={page}
        onPageChange={(newPage) => setPage(newPage)}
      />
      <DrawerRegistros
        open={drawerOpen}
        contribuyenteId={selectedId}
        onClose={handleCloseDrawer}
      />
    </div>
  );
}
