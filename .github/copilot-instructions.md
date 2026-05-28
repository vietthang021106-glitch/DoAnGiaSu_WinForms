# Copilot Instructions

## Project Guidelines
- User wants all tutor-related code updated consistently with new GIASU columns and UI flow.
- When updating this project, do not use workspace search/scan commands like rg/Get-ChildItem; only work from explicitly provided files or directly specified file paths.
- For this project, slot-limit check in BaiDangDAL.VuotGioiHan4Lop must use exactly DANGKYNHANLOP LEFT JOIN GIAODICH SQL provided by user; do not add UNION with BAIDANG.
- Commission payment status flow is as follows:
  1. When a tutor submits payment proof in FormThanhToan, CapNhatAnhChuyenKhoan sets GIAODICH TrangThai to 'ChoAdminDuyet'.
  2. Admin loads pending commissions from LayBaiChoDuyetPhi, filtering for TrangThai = 'ChoAdminDuyet'.
  3. When Admin clicks Xác nhận, XacNhanHoaHong sets both GIAODICH and BAIDANG TrangThai to 'DaGiao' with NgayXacNhan timestamp.
  4. When Admin rejects, TuChoiHoaHong resets GIAODICH to 'ChuaNop' and BAIDANG back to 'DangGiaoDich'.
- No database schema changes are needed; only the existing TrangThai column is utilized.

## Code Style
- No comments are allowed in any C# files created or modified.