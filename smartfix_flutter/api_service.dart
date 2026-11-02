import 'dart:convert';
import 'package:http/http.dart' as http;

/// خدمة الربط مع نظام SmartFix Backend
/// هذا الملف يتم استخدامه في تطبيق Flutter لإرسال واستقبال البيانات من الـ API
class ApiService {
  // ملاحظة: استبدل localhost برقم الـ IP الخاص بجهازك إذا كنت تختبر على محاكي أندرويد
  static const String baseUrl = 'http://10.0.2.2:5000/api';

  // الحصول على قائمة التذاكر
  Future<List<dynamic>> getMyTickets() async {
    final response = await http.get(Uri.parse('$baseUrl/tickets'));

    if (response.statusCode == 200) {
      return json.decode(response.body);
    } else {
      throw Exception('فشل في جلب التذاكر من السيرفر');
    }
  }

  // إنشاء طلب صيانة جديد من الجوال
  Future<bool> createTicket(int customerId, int deviceId, String description, String priority) async {
    final response = await http.post(
      Uri.parse('$baseUrl/tickets'),
      headers: {'Content-Type': 'application/json'},
      body: json.encode({
        'customerId': customerId,
        'deviceId': deviceId,
        'issueDescription': description,
        'priority': priority,
      }),
    );

    // الحالة 201 تعني أنه تم إنشاء الكائن بنجاح (Created)
    return response.statusCode == 201;
  }

  // تحديث حالة التذكرة (للفنيين)
  Future<bool> updateStatus(int ticketId, String status, int userId, String notes) async {
    final response = await http.patch(
      Uri.parse('$baseUrl/tickets/$ticketId/status'),
      headers: {'Content-Type': 'application/json'},
      body: json.encode({
        'newStatus': status,
        'userId': userId,
        'notes': notes,
      }),
    );

    return response.statusCode == 204; // No Content (تحديث ناجح بدون بيانات راجعة)
  }
}
