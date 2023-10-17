using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace DynVis.Core
{
	/// <summary>
	/// ����������� �����, ��������������� ��� ������������� ������ ������ -
	/// design-time/run-time.</summary>
	/// <remarks>���� ����� ������� �� ������� ����, ��� � ��������� �������, ����� ����� (���������) ���������
	/// � ���������, ������ ���������� ����� (��������� <see cref="IComponent.Site"/> ����� null).</remarks>
	public static class DesignerDetector
	{
		#region Const
		/// <summary>
		/// ����� ������ �����, ������� � �������� ���� <see cref="sm_Interface_Match"/>.</summary>
		/// <remarks>��� ������ �����, ��� ������� ����� ������ (�� ����� �������).</remarks>
		private const int START_FRAME = 4;
		#endregion // Const
		#region Static
		#region Fields
		/// <summary>
		/// � ������ <see cref="IsComponentInDesignMode"/> ����������
		/// ����� �� ����� ������� ����, ������� ��������� ������ ��������� (<see cref="IDesignerHost"/>).</summary>
		private static readonly Type sm_Interface_Match = typeof(IDesignerHost);
		#endregion // Fields
		#region Methods
		/// <summary>
		/// ���������, ��������� �� ��������� � ������ �������.</summary>
		/// <param name="component">���������.</param>
		/// <returns>true, ���� ��������� ������ � ���������.</returns>
		/// <remarks>� ���� ������ ����������
		/// ����� �� ����� ������� ����, ������� ��������� ��������� (<see cref="sm_Interface_Match"/>).</remarks>
		public static bool IsComponentInDesignMode(IComponent component)
		{
			// ����� ��� ������?
			if( component.Site != null )
			{
				return component.Site.DesignMode;
			}
			// ������ ��� ��� �� ��� ������...
			var _stackTrace = new StackTrace();
			int _iFrameCount = _stackTrace.FrameCount - 1;
			// ��������� ���� �������, ���� �� ������ ���, ����������� ��������� IDesignerHost
			for( int _iFrame = START_FRAME; _iFrame < _iFrameCount; _iFrame++ )
			{
				Type _type_FromStack = _stackTrace.GetFrame(_iFrame).GetMethod().DeclaringType;
				if( sm_Interface_Match.IsAssignableFrom(_type_FromStack) )
				{
					return true;
				}
			}
			// ��������� ���� ������� ��� ���������� IDesignerHost �� ���������� ��� ����������
			return false;
		}
		#endregion // Methods
		#endregion // Static
		#region Constructors

	    #endregion // Constructors
	} // DesignerDetector
}
