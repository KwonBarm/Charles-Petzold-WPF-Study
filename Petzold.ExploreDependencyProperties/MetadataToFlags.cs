using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Petzold.ExploreDependencyProperties
{

    // 바인딩 된 의존성 속성이 FrameworkPropertyMetadataOptions 값 중에서 어떤 옵션이 True인지를 확인하려는 경우 사용
    internal class MetadataToFlags : IValueConverter
    {
        public object Convert(object obj, Type type, object param, CultureInfo culture)
        {
            // WPF 의존성 속성에서 사용되는 FrameworkPropertyMetadata의 동작을 정의하는 플래그(enum)
            FrameworkPropertyMetadataOptions flags = 0;

            // FrameworkPropertyMetadata는 WPF의 DependencyProperty의 동작을 정의하는 메타데이터 클래스
            FrameworkPropertyMetadata metadate = obj as FrameworkPropertyMetadata;

            if(metadate == null)
                return null;

            // metadate.AffectsMeasure가 true이면 AffectsMeasure 속성이 Measure 단계에 영향을 준다는 것을 의미
            // flags |= FrameworkPropertyMetadataOptions.AffectsMeasure = flags | FrameworkPropertyMetadataOptions.AffectsMeasure
            // flags 변수에 기존값과 AffectsMeasure 값을 비트 OR 연산하여 저장
            if (metadate.AffectsMeasure)
                flags |= FrameworkPropertyMetadataOptions.AffectsMeasure;

            if(metadate.AffectsArrange)
                flags |= FrameworkPropertyMetadataOptions.AffectsArrange;

            if(metadate.AffectsParentMeasure)
                flags |= FrameworkPropertyMetadataOptions.AffectsParentMeasure;

            if(metadate.AffectsParentArrange)
                flags |= FrameworkPropertyMetadataOptions.AffectsParentArrange;

            if (metadate.AffectsRender)
                flags |= FrameworkPropertyMetadataOptions.AffectsRender;

            if(metadate.Inherits)
                flags |= FrameworkPropertyMetadataOptions.Inherits;

            if (metadate.OverridesInheritanceBehavior)
                flags |= FrameworkPropertyMetadataOptions.OverridesInheritanceBehavior;

            if (metadate.IsNotDataBindable)
                flags |= FrameworkPropertyMetadataOptions.NotDataBindable;

            if(metadate.BindsTwoWayByDefault)
                flags |= FrameworkPropertyMetadataOptions.BindsTwoWayByDefault;

            if (metadate.Journal)
                flags |= FrameworkPropertyMetadataOptions.Journal;

            return flags;
        }

        public object ConvertBack(object obj, Type type, object param, CultureInfo culture)
        {
            return new FrameworkPropertyMetadata(null, (FrameworkPropertyMetadataOptions)obj);
        }
    }
}
